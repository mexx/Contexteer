#I @"Source\packages\Fake.1.64.5\tools"
#r "FakeLib.dll"
#r "System.Web.Extensions.dll"

open Fake
open Fake.Git
open System.Collections.Generic
open System.Web.Script.Serialization

(* properties *)
let authors = ["Max Malook"]
let projectName = "Contexteer"
let copyright = "Copyright - Contexteer 2012"

TraceEnvironmentVariables()

let version =
    if hasBuildParam "version" then getBuildParam "version" else
    if isLocalBuild then getLastTag() else
    // version is set to the last tag retrieved from GitHub Rest API
    // see http://developer.github.com/v3/repos/ for reference
    let url = sprintf "https://api.github.com/repos/mexx/%s/tags" projectName
    tracefn "Downloading tags from %s" url
    let tagsFile = REST.ExecuteGetCommand null null url
    let tags = (new JavaScriptSerializer()).DeserializeObject(tagsFile) :?> System.Object array
    [ for tag in tags -> tag :?> Dictionary<string, System.Object> ]
        |> List.map (fun m -> m.Item("name") :?> string)
        |> List.max

let NugetKey = getBuildParamOrDefault "nugetkey" ""

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
            AssemblyInformationalVersion = version;
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
            Version = version
            OutputPath = nugetDir
            AccessKey = NugetKey
            Publish = NugetKey <> "" })
        nuspecPath

    !! (nugetDir + "Contexteer.*.nupkg")
      |> CopyTo deployDir
)

Target "Default" DoNothing
Target "Deploy" DoNothing

// Build order
"Clean"
  ==> "SetAssemblyInfo"
  ==> "BuildApp"
  ==> "Test"
  ==> "BuildZip"
  ==> "BuildNuGet"
  ==> "Deploy"
  ==> "Default"

// start build
RunParameterTargetOrDefault "target" "Default"