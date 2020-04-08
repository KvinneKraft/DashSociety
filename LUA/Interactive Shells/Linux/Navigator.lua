
--[[
    To make simple usage of tools easier in a Linux Pentest Environment.
    -Dashie
]]--

function getarrlen(arr) local len = 0 for id in pairs(arr) do len = len + 1 end return len end

function main()--I will continue some othjer time, me bored of lua here.
    local options = {"~==========================================~",
                     "sqlmap    -===-    sqlmap -u %host% --dbs --tables --columns --dump-all --tor --random-agent --technique=BEUS --threads=5 --output-dir=\"/Sqlmap Results\"",
                     "nmap      -===-    nmap %host% ",
                     "~==========================================~"};

    for key = 1, getarrlen(options) do io.write(options[key].."\n") end

    while true do
        io.write("$(Navigator Selector)> ")
        local option = io.read()
    end
end

main()
