local smtp = require("socket.smtp")

from = "<dashsociety@mail.ch>"
rcpt = "KvinneKraft@protonmail.com"

mess =
{
    headers =
    {
        to = "Dash Society <KvinneKraft@protonmail.com>"
        cc = '"Dash Society <dashsociety@mail.ch>"',
        subject = "Yes!"
    },

    body = "How are you feeling?"
}

r, e = smtp.send { from = from, rcpt = rcpt, source = smtp.message(mess) }

io.read()
