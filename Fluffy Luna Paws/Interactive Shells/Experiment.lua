
local url = "https://pugpawz.com/data/image.jpeg";
local arr = {}

local id = 1;

for token in string.gmatch(url, '([^/]+)') do
	local key = 1
	io.write(token)
	arr[key] = token
	key = key .. 1
	id = key
end

local name = arr[table.getn(arr)]

io.write("\nName: " .. name .. "\n")
