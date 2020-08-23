
// Author: Dashie
// Version: 1.0

using System;
using System.Media;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Lunarilicious
{
    public class Sounds
    {
	private readonly List<SoundPlayer> SoundPlayers = new List<SoundPlayer>();

	public void LoadSoundData()
	{
	    List<string> paths = new List<string>();

	    if (SoundPlayers.Count > 0)
	    {
		SoundPlayers.Clear();
	    };

	    foreach (SoundType sound in Enum.GetValues(typeof(SoundType)))
	    {
		SoundPlayers.Add(new SoundPlayer("data\\sounds\\" + sound.ToString() + ".wav"));
	    };
	}

	public enum SoundType
	{
	    DogAttack,
	    DogWalk,
	    DogGrowl
	};

	private readonly Form Owner = Lunaroc.getOwner();

	public void playSound(SoundType sound_id)
	{
	    Owner.Invoke
	    (
		(MethodInvoker)delegate ()
		{
		    SoundPlayers[(int)sound_id].Play();
		}
	    );
	}
    };
};