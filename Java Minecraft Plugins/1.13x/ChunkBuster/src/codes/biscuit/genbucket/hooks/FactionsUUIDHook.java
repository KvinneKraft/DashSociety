// 
// Decompiled by Procyon v0.5.36
// 

package codes.biscuit.genbucket.hooks;

import com.massivecraft.factions.struct.Relation;
import com.massivecraft.factions.iface.RelationParticipator;
import com.massivecraft.factions.Faction;
import com.massivecraft.factions.Board;
import com.massivecraft.factions.FLocation;
import org.bukkit.Location;
import com.massivecraft.factions.FPlayer;
import com.massivecraft.factions.FPlayers;
import org.bukkit.entity.Player;

class FactionsUUIDHook
{
    boolean hasFaction(final Player p) {
        final FPlayer fPlayer = FPlayers.getInstance().getByPlayer(p);
        return fPlayer.hasFaction();
    }
    
    boolean isNotWilderness(final Location loc) {
        final FLocation fLoc = new FLocation(loc);
        final Faction fLocFaction = Board.getInstance().getFactionAt(fLoc);
        return !fLocFaction.isWilderness();
    }
    
    boolean locationIsFactionClaim(final Location loc, final Player p) {
        final Faction locFaction = Board.getInstance().getFactionAt(new FLocation(loc));
        final Faction pFaction = FPlayers.getInstance().getByPlayer(p).getFaction();
        return locFaction.equals(pFaction);
    }
    
    boolean isFriendlyPlayer(final Player p, final Player otherP) {
        final FPlayer fPlayer = FPlayers.getInstance().getByPlayer(p);
        final FPlayer otherFPlayer = FPlayers.getInstance().getByPlayer(otherP);
        final Relation relation = fPlayer.getRelationTo((RelationParticipator)otherFPlayer);
        return relation == Relation.MEMBER || relation == Relation.ALLY || relation == Relation.TRUCE;
    }
}
