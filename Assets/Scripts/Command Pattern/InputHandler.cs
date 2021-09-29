using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern
{
    public class InputHandler : MonoBehaviour
    {
        // box yang akan ditransform
        public Transform boxTrans;

        // key yang digunakan
        private Command buttonW, buttonS, buttonA, buttonD, buttonB, buttonZ, buttonR;

        // simpan semua command dalam List
        public static List<Command> oldCommands = new List<Command>();

        // box start position
        private Vector3 boxStartPos;

        // reset coroutine
        private Coroutine replayCoroutine;

        // shoud we start replay
        public static bool shouldStartReplay;

        // status
        private bool isReplaying;

        void Start()
        {
            buttonB = new DoNothing();
            buttonW = new MoveForward();
            buttonS = new MoveReverse();
            buttonA = new MoveLeft();
            buttonD = new MoveRight();
            buttonZ = new UndoCommand();
            buttonR = new ReplayCommand();

            boxStartPos = boxTrans.position;
        }

        void Update()
        {
            // selama tidak replay, handle input seperti biasa
            if (!isReplaying)
            {
                HandleInput();
            }

            StartReplay();
        }

        // mengecek jika kita memencet key, jika iya lakukan sesuai aksi yang dibind pada key
        public void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                buttonA.Execute(boxTrans, buttonA);
            }
            else if(Input.GetKeyDown(KeyCode.B))
            {
                buttonB.Execute(boxTrans, buttonB);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                buttonD.Execute(boxTrans, buttonD);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                buttonR.Execute(boxTrans, buttonZ);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                buttonS.Execute(boxTrans, buttonS);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                buttonW.Execute(boxTrans, buttonW);
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                buttonZ.Execute(boxTrans, buttonZ);
            }
        }

        // cek apakah harus melakukan replay
        void StartReplay()
        {
            if(shouldStartReplay && oldCommands.Count > 0)
            {
                shouldStartReplay = false;

                // stop coroutine semua replay agar mulai dari awal
                if (replayCoroutine != null)
                {
                    StopCoroutine(replayCoroutine);
                }

                // jalankan replay
                replayCoroutine = StartCoroutine(ReplayCommands(boxTrans));
            }
        }

        // coroutine replay
        IEnumerator ReplayCommands(Transform boxTrans)
        {
            isReplaying = true;

            // pindah box ke start position lalu jalankan replay berdasarkan old commands di list
            boxTrans.position = boxStartPos;
            for (int i = 0; i < oldCommands.Count; i++)
            {
                oldCommands[i].Move(boxTrans);

                yield return new WaitForSeconds(0.3f); //???
            }

            //replay ended
            isReplaying = false;
        }
    }
}
