param([string]$version=1.0.0)
$sfbDir = (get-item $PSScriptRoot )
$appConfigFile = [IO.Path]::Combine($sfbDir, 'Objectivity.Bot.HtmlBuilder\Objectivity.Bot.HtmlBuild.nuspec')
$appConfig = New-Object XML;
$appConfig.Load($appConfigFile);
$appConfig.package.metadata.version = "$version";
$appConfig.Save($appConfigFile);