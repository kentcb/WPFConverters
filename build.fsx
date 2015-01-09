#I "Src/packages/FAKE.3.13.3/tools"
#r "FakeLib.dll"

open Fake
open Fake.AssemblyInfoFile
open Fake.EnvironmentHelper
open Fake.MSBuildHelper
open Fake.NuGetHelper
open Fake.XUnitHelper

// properties
let semanticVersion = "1.3.1"
let version = (>=>) @"(?<major>\d*)\.(?<minor>\d*)\.(?<build>\d*).*?" "${major}.${minor}.${build}.0" semanticVersion
let configuration = getBuildParamOrDefault "configuration" "Release"
let deployToNuGet = getBuildParamOrDefault "deployToNuGet" "false"
let genDir = "Gen/"
let docDir = "Doc/"
let srcDir = "Src/"
let testDir = genDir @@ "Test"
let nugetDir = genDir @@ "NuGet"

type Platform = { ConfigurationPrefix: string; NuGetName: string }

let platforms =
    [
        { ConfigurationPrefix = "SL40 "; NuGetName = "sl40" };
        { ConfigurationPrefix = "SL50 "; NuGetName = "sl50" };
        { ConfigurationPrefix = "FX35 "; NuGetName = "net35" };
        { ConfigurationPrefix = "FX40 "; NuGetName = "net40" };
        { ConfigurationPrefix = "FX45 "; NuGetName = "net45" }
    ]

Target "Clean" (fun _ ->
    CleanDirs[genDir; testDir; nugetDir]

    for platform in platforms do
        build (fun p ->
            { p with
                Verbosity = Some(Quiet)
                Targets = ["Clean"]
                Properties = ["Configuration", platform.ConfigurationPrefix + configuration]
            })
            (srcDir @@ "WpfConverters.sln")
)

Target "Build" (fun _ ->
    // generate the shared assembly info
    CreateCSharpAssemblyInfoWithConfig (srcDir @@ "AssemblyInfoCommon.cs")
        [
            Attribute.Version version
            Attribute.FileVersion version
            Attribute.Configuration configuration
            Attribute.Company "Kent Boogaart"
            Attribute.Product "WPF Converters"
            Attribute.Copyright "© Copyright. Kent Boogaart."
            Attribute.Trademark ""
            Attribute.Culture ""
            Attribute.CLSCompliant true
            Attribute.StringAttribute("NeutralResourcesLanguage", "en-US", "System.Resources")
            Attribute.StringAttribute("AssemblyInformationalVersion", semanticVersion, "System.Reflection")
        ]
        (AssemblyInfoFileConfig(false))

    for platform in platforms do
        build (fun p ->
            { p with
                Verbosity = Some(Quiet)
                Targets = ["Build"]
                Properties =
                    [
                        "Optimize", "True"
                        "DebugSymbols", "True"
                        "Configuration", platform.ConfigurationPrefix + configuration
                    ]
            })
            (srcDir @@ "WpfConverters.sln")
)

Target "ExecuteUnitTests" (fun _ ->
    xUnit (fun p ->
        { p with
            ShadowCopy = false;
            HtmlOutput = true;
            XmlOutput = true;
            OutputDir = testDir
        })
        [srcDir @@ "Kent.Boogaart.Converters.UnitTests/bin" @@ "FX45 " + configuration @@ "Kent.Boogaart.Converters.UnitTests.dll"]
)

Target "CreateArchives" (fun _ ->
    // source archive
    !! "**/*.*"
        -- ".git/**"
        -- (genDir @@ "**")
        -- (srcDir @@ "packages/**/*")
        -- (srcDir @@ "**/*.suo")
        -- (srcDir @@ "**/*.csproj.user")
        -- (srcDir @@ "**/*.gpState")
        -- (srcDir @@ "**/bin/**")
        -- (srcDir @@ "**/obj/**")
        |> Zip "." (genDir @@ "WpfConverters-" + semanticVersion + "-src.zip")

    // binary archive
    for platform in platforms do
        let workingDir = srcDir @@ "Kent.Boogaart.Converters/bin" @@ platform.ConfigurationPrefix + configuration

        !! (workingDir @@ "Kent.Boogaart.Converters.*")
            |> Zip workingDir (genDir @@ "WpfConverters-" + semanticVersion + "-" + platform.ConfigurationPrefix + "-bin.zip")
)

Target "CreateNuGetPackages" (fun _ ->
    // copy binaries to lib
    for platform in platforms do
        !! (srcDir @@ "Kent.Boogaart.Converters/bin" @@ platform.ConfigurationPrefix + configuration @@ "Kent.Boogaart.Converters.*")
            |> CopyFiles (nugetDir @@ "lib" @@ platform.NuGetName)

    // copy source to src
    [!! (srcDir @@ "**/*.*")
        -- (srcDir @@ "packages/**/*")
        -- (srcDir @@ "**/*.suo")
        -- (srcDir @@ "**/*.csproj.user")
        -- (srcDir @@ "**/*.gpState")
        -- (srcDir @@ "**/bin/**")
        -- (srcDir @@ "**/obj/**")]
        |> CopyWithSubfoldersTo nugetDir

    // create the NuGets
    NuGet (fun p ->
        {p with
            Project = "Kent.Boogaart.Converters"
            Version = semanticVersion
            OutputPath = nugetDir
            WorkingDir = nugetDir
            SymbolPackage = NugetSymbolPackage.Nuspec
            Publish = System.Convert.ToBoolean(deployToNuGet)
        })
        (srcDir @@ "WpfConverters.nuspec")
)

// build order
"Clean"
    ==> "Build"
    ==> "ExecuteUnitTests"
    ==> "CreateArchives"
    ==> "CreateNuGetPackages"

RunTargetOrDefault "CreateNuGetPackages"