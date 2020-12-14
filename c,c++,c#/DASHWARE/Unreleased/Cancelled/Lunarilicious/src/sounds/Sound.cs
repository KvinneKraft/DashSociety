
// Author: Dashie
// Version: 1.0

using System;
using System.IO;
using System.Text;
using System.Media;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Lunarilicious
{
    class Sound
    {
	public static class Game
	{
	    public static bool allowAudio = true;
	    public static List<SoundPlayer> soundPlayer = new List<SoundPlayer>();
	};

	static void LoadGameTracks()
	{
	    foreach (var track_file in new DirectoryInfo("data\\sounds\\tracks\\").GetFiles("*.wav"))
	    {
		Game.soundPlayer.Add(new SoundPlayer(track_file.FullName));
	    };

	    new Thread
	    (
		() => 
		{
		    Random rand = new Random();

		    while (Game.allowAudio)
		    {
			Game.soundPlayer[rand.Next(Game.soundPlayer.Count)].PlaySync();
		    };
		}
	    )

	    { IsBackground = true, Name = "MusicThread" }.Start();
	}
    };
};
