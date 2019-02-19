open Fake

let testDirectory = getBuildParamOrDefault "buildMode" "Debug"
let nUnitRunner = "nunit3-console.exe"
let mutable nUnitToolPath = @"tools\NUnit.ConsoleRunner\"
let acceptanceTestPlayList = getBuildParamOrDefault "playList" ""
let nunitTestFormat = getBuildParamOrDefault "nunitTestFormat" "nunit2"

Target "Restore Solution Packages" (fun _ ->
     "./SFA.DAS.Events.sln"
     |> RestoreMSSolutionPackages (fun p ->
         { p with
             OutputPath = ".\\packages"
             Retries = 4 })
 )