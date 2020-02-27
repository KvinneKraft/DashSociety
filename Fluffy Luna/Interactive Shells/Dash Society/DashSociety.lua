
-- Dash Society is a client for any operating system
-- that has LUA 5.2 installed.

-- Author: Dashie
-- Version: 1.0


function get_directory()
    local chr = os.tmpname():sub(1, 1)

    if (chr == "/") 
        then
            chr = "/[^/]*$"
        end
    else
        chr = "\\^[\\]*$"
    end

    return arg[0]:sub(1, arg[0]:find(chr) .. ("DashSociety.lua" or ''))
end

function sys(message)
    io.write("[info]: " .. message .. " \n");
end

function pr(message)
    io.write(message)
end

function execute_command(command)
    if (command == "shell")
        then
            while (true) do
                sys("You are now in the shell of the Dash.")
                sys("Type \'!bye\' at any time in order to go back.\n")

                pr("$[/Dash Shell/]: ")
            end
        end 
    else if (command == "cls")
        then
            os.execute("clear")
        end
    else
        sys("command is unknown, use ? for help")
    end
end

function main()
    while (true) do
        
    end
end