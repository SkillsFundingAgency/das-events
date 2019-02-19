open Fake

let testDirectory = getBuildParamOrDefault "buildMode" "Debug"
let nUnitRunner = "nunit3-console.exe"
let mutable nUnitToolPath = @"tools\NUnit.ConsoleRunner\"
let acceptanceTestPlayList = getBuildParamOrDefault "playList" ""
let nunitTestFormat = getBuildParamOrDefault "nunitTestFormat" "nunit2"

Target "Dotnet Restore" (fun _ ->
    DotNetCli.Restore(fun p ->
        { p with
                Project = ".\\SFA.DAS.Events.Api" })
    DotNetCli.Restore(fun p ->
        { p with
                Project = ".\\SFA.DAS.Events.Api.Client" })
    DotNetCli.Restore(fun p ->
        { p with
                Project = ".\\SFA.DAS.Events.Api.Client.UnitTests" })
    DotNetCli.Restore(fun p ->
        { p with
                Project = ".\\SFA.DAS.Events.Api.Types" })
    DotNetCli.Restore(fun p ->
        { p with
                Project = ".\\SFA.DAS.Events.Api.UnitTests" })
    DotNetCli.Restore(fun p ->
        { p with
                Project = ".\\SFA.DAS.Events.Application" })
    DotNetCli.Restore(fun p ->
        { p with
                Project = ".\\SFA.DAS.Events.Application.UnitTests" })
    DotNetCli.Restore(fun p ->
        { p with
                Project = ".\\SFA.DAS.Events.Domain" })
    DotNetCli.Restore(fun p ->
        { p with
                Project = ".\\SFA.DAS.Events.Infrastructure" })

)