#I @"Source\packages\Fake.1.64.5\tools"
#r "FakeLib.dll"

open Fake

(* properties *)
let authors = ["Max Malook"]
let projectName = "Contexteer"
let copyright = "Copyright - Contexteer 2012"

TraceEnvironmentVariables()

let version = if isLocalBuild then getBuildParamOrDefault "version" "0.0.0.1" else buildVersion
let packageVersion = getBuildParamOrDefault "packageVersion" version

let NugetKey = getBuildParamOrDefault "nuget.key" ""

(* Directories *)
let targetPlatformDir = getTargetPlatformDir "v4.0.30319"
let sourceDir = @".\Source\"
let packagesDir = sourceDir + @"packages\"

let buildDir = @".\Build\"
let testDir = buildDir
let testOutputDir = buildDir + @"Specs\"
let nugetDir = buildDir + @"NuGet\"
let deployDir = @".\Release\"

(* files *)
let slnReferences = !! (sourceDir + @"*.sln")
let nugetPath = sourceDir + @".nuget\NuGet.exe"
let nuspecPath = sourceDir + "Contexteer\Contexteer.nuspec"

(* tests *)
let MSpecVersion = lazy ( GetPackageVersion packagesDir "Machine.Specifications" )
let mspecTool = lazy( sprintf @"%s\Machine.Specifications.%s\tools\mspec-clr4.exe" packagesDir (MSpecVersion.Force()) )

(* Targets *)
Target "Clean" (fun _ -> 
    CleanDirs [buildDir; testDir; testOutputDir; nugetDir; deployDir]
)

Target "SetAssemblyInfo" (fun _ ->
    AssemblyInfo
        (fun p ->
        {p with
            CodeLanguage = CSharp;
            AssemblyVersion = version;
            AssemblyInformationalVersion = packageVersion;
            AssemblyTitle = "Contexteer";
            AssemblyDescription = "A framework for contexts";
            AssemblyCompany = projectName;
            AssemblyCopyright = copyright;
            Guid = "fab4c86e-fa7a-453c-acb6-90e229411626";
            OutputFileName = @".\Source\Contexteer\Properties\AssemblyInfo.cs"})
)

Target "BuildApp" (fun _ ->
    MSBuildRelease buildDir "Build" slnReferences
        |> Log "AppBuild-Output: "
)

Target "Test" (fun _ ->
    ActivateFinalTarget "DeployTestResults"
    !+ (testDir + "/*.Specs.dll")
      ++ (testDir + "/*.Examples.dll")
        |> Scan
        |> MSpec (fun p ->
                    {p with
                        ToolPath = mspecTool.Force()
                        HtmlOutputDir = testOutputDir})
)

FinalTarget "DeployTestResults" (fun () ->
    !+ (testOutputDir + "\**\*.*")
      |> Scan
      |> Zip testOutputDir (sprintf "%sMSpecResults.zip" deployDir)
)

Target "BuildZip" (fun _ ->
    !+ (buildDir + "/**/*.*")
      -- "*.zip"
      -- "**/*.Specs.dll"
      -- "**/*.Specs.pdb"
        |> Scan
        |> Zip buildDir (deployDir @@ sprintf "%s-%s.zip" projectName version)
)

Target "BuildNuGet" (fun _ ->
    let nugetLibDir = nugetDir @@ "lib" @@ "4.0"

    CleanDirs [nugetLibDir]

    [buildDir @@ "Contexteer.dll"]
        |> CopyTo nugetLibDir

    NuGet (fun p ->
        {p with
            ToolPath = nugetPath
            Authors = authors
            Project = projectName
            Version = packageVersion
            OutputPath = nugetDir
            AccessKey = NugetKey
            Publish = NugetKey <> "" })
        nuspecPath

    !! (nugetDir + "Contexteer.*.nupkg")
      |> CopyTo deployDir
)

Target "Default" DoNothing

// Build order
"Clean"
  ==> "SetAssemblyInfo"
  ==> "BuildApp"
  ==> "Test"
  ==> "BuildZip"
  ==> "BuildNuGet"
  ==> "Default"

// start build
RunParameterTargetOrDefault "target" "Default"