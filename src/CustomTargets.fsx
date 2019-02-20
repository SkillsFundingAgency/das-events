open Fake

let testDirectory = getBuildParamOrDefault "buildMode" "Debug"
let nUnitRunner = "nunit3-console.exe"
let mutable nUnitToolPath = @"tools\NUnit.ConsoleRunner\"
let acceptanceTestPlayList = getBuildParamOrDefault "playList" ""
let nunitTestFormat = getBuildParamOrDefault "nunitTestFormat" "nunit2"

Target "Restore Solution Packages" (fun _ ->
    DotNetCli.Restore(fun p ->
        { p with
                Project = ".\\SFA.DAS.Events.Api.Client\SFA.DAS.Events.Api.Client.csproj" })
    DotNetCli.Restore(fun p ->
        { p with
                Project = ".\\SFA.DAS.Events.Api.Types\SFA.DAS.Events.Api.Types.csproj" })
 )