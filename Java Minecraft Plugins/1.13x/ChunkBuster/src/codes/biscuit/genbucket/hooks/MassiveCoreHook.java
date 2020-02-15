// 
// Decompiled by Procyon v0.5.36
// 

package codes.biscuit.genbucket.hooks;

import com.massivecraft.factions.Rel;
import com.massivecraft.factions.RelationParticipator;
import com.massivecraft.factions.entity.Faction;
import com.massivecraft.massivecore.ps.PS;
import com.massivecraft.factions.entity.BoardColl;
import org.bukkit.Location;
import com.massivecraft.factions.entity.MPlayer;
import org.bukkit.entity.Player;

class MassiveCoreHook
{
    boolean hasNoFaction(final Player p) {
        return !MPlayer.get((Object)p).hasFaction();
    }
    
    boolean isWilderness(final Location loc) {
        return BoardColl.get().getFactionAt(PS.valueOf(loc)).isNone();
    }
    
    boolean locationIsNotFaction(final Location loc, final Player p) {
        final Faction locFaction = BoardColl.get().getFactionAt(PS.valueOf(loc));
        final Faction pFaction = MPlayer.get((Object)p).getFaction();
        return !locFaction.equals(pFaction);
    }
    
    boolean isFriendlyPlayer(final Player p, final Player otherP) {
        final MPlayer mPlayer = MPlayer.get((Object)p);
        final MPlayer otherMPlayer = MPlayer.get((Object)otherP);
        final Rel relation = mPlayer.getRelationTo((RelationParticipator)otherMPlayer);
        return relation == Rel.MEMBER || relation == Rel.ALLY || relation == Rel.TRUCE;
    }
}
