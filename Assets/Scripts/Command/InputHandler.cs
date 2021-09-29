using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerShooting playerShooting;

    // List command
    Queue<Command> commands = new Queue<Command>();

    private void FixedUpdate()
    {
        // input movement
        Command moveCommand = InputMovementHandler();

        if(moveCommand != null)
        {
            // simpan dalam queue
            commands.Enqueue(moveCommand);
            moveCommand.Execute();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // handle shoot
        Command shootCommand = InputShootHandler();
    }

    Command InputMovementHandler()
    {
        // input miring
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            return new MoveCommand(playerMovement, 1, 1);
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            return new MoveCommand(playerMovement, -1, 1);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            return new MoveCommand(playerMovement, 1, -1);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            return new MoveCommand(playerMovement, -1, -1);
        }
        
        // cek key apa yang diinput
        // forward

        else if (Input.GetKey(KeyCode.W))
        {
            return new MoveCommand(playerMovement, 0, 1);
        }
        // left
        else if (Input.GetKey(KeyCode.A))
        {
            return new MoveCommand(playerMovement, -1, 0);
        }
        // reverse
        else if (Input.GetKey(KeyCode.S))
        {
            return new MoveCommand(playerMovement, 0, -1);
        }
        // right
        else if (Input.GetKey(KeyCode.D))
        {
            return new MoveCommand(playerMovement, 1, 0);
        }

        

        // undo
        else if (Input.GetKey(KeyCode.Z))
        {
            return Undo();
        }
        else
        {
            return new MoveCommand(playerMovement, 0, 0);
        }
    }

    Command Undo()
    {
        // jika queue ada isinya, lakukan undo
        if (commands.Count > 0)
        {
            Command undoCommand = commands.Dequeue();
            undoCommand.UnExecute();
        }
        return null;
    }

    Command InputShootHandler()
    {
        // jika trigger shoot dipencet
        if (Input.GetButtonDown("Fire1"))
        {
            return new ShootCommand(playerShooting);
        }
        else
        {
            return null;
        }
    }
}
