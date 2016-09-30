@echo off
for /r %%i in (*.png) do (
	echo "converting %%i"
	c:\dev\pngquant\pngquant.exe %%i --output %%i.new --force
)