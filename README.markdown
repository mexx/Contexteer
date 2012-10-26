# What is Contexteer?

Contexteer is little framework build to support you when you need a context in your code.

For library writers it provides a standard way to add multi tenancy support.

## How to use it

Create a class that names your context and implements the IContext interface.

	public class BusinessBranch : IContext
	{
		public string Name { get; private set; }
		
		public BusinessBranch(string name)
		{
			Name = name;
		}
	}

Create an instance of your context.

	var branch = new BusinessBranch("Headquarter");

A library which uses Contexteer will provide some way to use this instance.
Here an example from [FeatureSwitcher](https://github.com/mexx/FeatureSwitcher/)
	
	Feature<Sample>.Is().EnabledInContextOf(branch);

For configuration purposes Contexteer provides the starting point for a fluent API library writers can extend.
Again an example from FeatureSwitcher

	In<BusinessBranch>.Contexts.FeaturesAre().AlwaysEnabled();

To provide similar configuration functionality simply write an extension method for ConfigurationOf<TContext> class.
One more example from FeatureSwitcher

	public static IConfigureFeaturesFor<TContext> FeaturesAre<TContext>(this ConfigurationOf<TContext> This)
	    where TContext : IContext
	{
	    var result = new FeatureConfigurationFor<TContext>();
	    This.Set(typeof (FeatureConfiguration), result);
	    return result;
	}

## How to get it

### NuGet package manager

Type

	install-package Contexteer

into the package management console.

### Build from source

Just download the repository from github and run the build.cmd (or build.NoGit.cmd if you don't have git installed). The build of Contexteer only requires the .NET Framework 4.0 to be installed on your machine. Everything else should work out-of-the-box. If not, please take the time to add an issue to this project. After a successful build you find all the assemblies in a zip file under the "Release" folder.

## How to contribute code

* Login in github (you need an account)
* Fork the main repository from [Github](https://github.com/mexx/Contexteer)
* Please contribute only on the dev branch. (There is no development on the master branch. Only the releases are build from there)
* Push your changes to your GitHub repository.
* Send a pull request

## Versioning

Versioning follows the [Semantic Versioning Specification](http://semver.org/).
