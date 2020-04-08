-- DashSociety

function write() local source = "x = msgbox(\"Dash Society has pwned you sir!\", 16, \"DashSociety\")" local file = io.open("popup.vbs", "w") io.output(file) io.write(source) io.close(file)  end
function run() os.execute("C:\\Windows\\System32\\cmd.exe /C \"C:\\Windows\\System32\\wscript.exe\" popup.vbs") end
write() while true do run() end
