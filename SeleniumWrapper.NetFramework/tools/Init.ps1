param($installPath, $toolsPath, $package, $project)

$file1 = $project.ProjectItems.Item("content/chromedriver.exe")
$file2 = $project.ProjectItems.Item("content/KillWithParent.fsx")

# set 'Copy To Output Directory' to 'Copy if newer'
$copyToOutput1 = $file1.Properties.Item("CopyToOutputDirectory")
$copyToOutput1.Value = 1

$copyToOutput2 = $file2.Properties.Item("CopyToOutputDirectory")
$copyToOutput2.Value = 2