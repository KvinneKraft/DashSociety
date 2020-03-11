local filename = "Walshy.vbs"

function popup(message, title)
  local filecode = "x = MsgBox(\""..message.."\", 48, \""..title.."\")"
  os.execute("echo "..filecode.." >"..filename.." && C:\\Windows\\System32\\wscript.exe "..filename.."");
end

while true do
	popup("DashSociety Really Hates You Sir Also Get pwned!", "Dash Society");
end
