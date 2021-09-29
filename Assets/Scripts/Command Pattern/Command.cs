using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CommandPattern
{
    // parent class
    public abstract class Command
    {
        // sejauh apa box bergerak
        protected float moveDistance = 1f;

        // bergerak lalu save command
        public abstract void Execute(Transform boxTrans, Command command);

        // undo command
        public virtual void Undo(Transform boxTrans) { }

        // Gerakkan cube
        public virtual void Move(Transform boxTrans) { }
    }

    // child class
    public class MoveForward : Command
    {
        // ketika memencet key
        // bergerak lalu save command
        public override void Execute(Transform boxTrans, Command command)
        {
            // gerakka cube
            Move(boxTrans);

            //save command ke oldCommands
            InputHandler.oldCommands.Add(command);
        }

        // undo command
        public override void Undo(Transform boxTrans) {
            boxTrans.Translate(-boxTrans.forward * moveDistance);
        }

        // Gerakkan cube
        public override void Move(Transform boxTrans) {
            boxTrans.Translate(boxTrans.forward * moveDistance);
        }
    }

    public class MoveReverse : Command
    {
        // ketika memencet key
        // bergerak lalu save command
        public override void Execute(Transform boxTrans, Command command)
        {
            // gerakka cube
            Move(boxTrans);

            //save command ke oldCommands
            InputHandler.oldCommands.Add(command);
        }

        // undo command. kebalikan dari forward, undonya bernilai positif
        public override void Undo(Transform boxTrans)
        {
            boxTrans.Translate(boxTrans.forward * moveDistance);
        }

        // Gerakkan cube
        public override void Move(Transform boxTrans)
        {
            boxTrans.Translate(-boxTrans.forward * moveDistance);
        }
    }

    public class MoveRight : Command
    {
        // ketika memencet key
        // bergerak lalu save command
        public override void Execute(Transform boxTrans, Command command)
        {
            // gerakka cube
            Move(boxTrans);

            //save command ke oldCommands
            InputHandler.oldCommands.Add(command);
        }

        // undo command.
        public override void Undo(Transform boxTrans)
        {
            boxTrans.Translate(-boxTrans.right * moveDistance);
        }

        // Gerakkan cube
        public override void Move(Transform boxTrans)
        {
            boxTrans.Translate(boxTrans.right * moveDistance);
        }
    }

    public class MoveLeft : Command
    {
        // ketika memencet key
        // bergerak lalu save command
        public override void Execute(Transform boxTrans, Command command)
        {
            // gerakka cube
            Move(boxTrans);

            //save command ke oldCommands
            InputHandler.oldCommands.Add(command);
        }

        // undo command. kebalikan dari right, undonya bernilai positif
        public override void Undo(Transform boxTrans)
        {
            boxTrans.Translate(boxTrans.right * moveDistance);
        }

        // Gerakkan cube
        public override void Move(Transform boxTrans)
        {
            boxTrans.Translate(-boxTrans.right * moveDistance);
        }
    }

    public class DoNothing : Command
    {
        public override void Execute(Transform boxTrans, Command command)
        {
            //do nothing
        }
    }

    public class UndoCommand : Command
    {
        // ketika memencet key
        // bergerak lalu save command
        public override void Execute(Transform boxTrans, Command command)
        {
            List<Command> oldCommands = InputHandler.oldCommands;

            // jika ada isinya
            if (oldCommands.Count > 0)
            {
                Command latestCommand = oldCommands[oldCommands.Count - 1];

                latestCommand.Undo(boxTrans);
                oldCommands.RemoveAt(oldCommands.Count - 1);
            }
        }
    }

    // replay semua command
    public class ReplayCommand : Command
    {
        public override void Execute(Transform boxTrans, Command command)
        {
            // set true agar input handler menjalankan replay
            InputHandler.shouldStartReplay = true;
        }
    }
}
