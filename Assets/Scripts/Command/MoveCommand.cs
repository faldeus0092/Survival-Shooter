using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : Command
{
    PlayerMovement playerMovement;
    float h, v; //horizontal & vertical

    public MoveCommand(PlayerMovement _playerMovement, float _h, float _v)
    {
        // set nilai pada class ini sesuai parameter yang diterima
        playerMovement = _playerMovement;
        h = _h;
        v = _v;
    }

    public override void Execute()
    {
        // gerakkan player
        playerMovement.Move(h, v);
        playerMovement.Animating(h, v);
    }

    public override void UnExecute()
    {
        // undo
        // inverse arah
        playerMovement.Move(-h, -v);
        playerMovement.Animating(h, v);
    }
}
