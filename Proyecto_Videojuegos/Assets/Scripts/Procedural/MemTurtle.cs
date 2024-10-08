using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void VoidFuncVoid();
public delegate void VoidFuncInt(int turn);
public delegate void VoidFunc4Param(Vector3 pos, Vector3 lastPos, int turn, int invTurn);

public class MemTurtle
{
    
    Vector3 pos = Vector3.zero;
    Vector3 lastPos = Vector3.zero;
    
    int turn = 0;
    int invTurn = 0;
    
    Vector3[] dirs = {Vector3.up, Vector3.right, Vector3.down, Vector3.left};

    // Límites del área
    float clampX = 0;
    float clampY = 0;

    // Getters
    public Vector3 Pos {get => pos;}
    public Vector3 LastPos {get => lastPos;}
    public Vector3 Dir {get => dirs[turn];}
    public int Turn {get => turn;}
    public int InvTurn {get => invTurn;}

    public VoidFunc4Param forwardDelegate;
    public VoidFunc4Param backwardDelegate;
    public VoidFuncInt turnDelegate;

    public MemTurtle(float clampX = 3, float clampY = 3)
    {
        forwardDelegate = (x, y, s, w) => { };
        backwardDelegate = (x, y, s, w) => { };
        turnDelegate = (i) => { };

        invTurn = (turn + 2) & 0x03;
        
        this.clampX = clampX;
        this.clampY = clampY;
    }

    public void Forward()
    {
        lastPos = pos;
        pos += dirs[turn];
        
        // Restringir la matriz 
        pos.x = Mathf.Clamp(pos.x, 0, clampX - 1);
        pos.y = Mathf.Clamp(pos.y, 0, clampY - 1);

        if (lastPos != pos)
        {
            forwardDelegate(pos, lastPos, turn, invTurn);
        }
    }
    
    public void Backward()
    {
        lastPos = pos;
        pos += dirs[invTurn];
        
        // Restringir la matriz 
        pos.x = Mathf.Clamp(pos.x, 0, clampX - 1);
        pos.y = Mathf.Clamp(pos.y, 0, clampY - 1);

        if (lastPos != pos)
        {
            backwardDelegate(pos, lastPos, turn, invTurn);
        }
    }

    // Invoca Forward() n veces
    public void Forward(int n)
    {
        for (int i = 1; i <= n; i++)
        {
            Forward();
        }
    }

    public void TurnTo(int newTurn)
    {
        turn = newTurn;
        turn &= 0x03;
        invTurn = (turn + 2) & 0x03;
        turnDelegate(turn);
    }
    
    public void AddTurn(int newTurn)
    {
        turn += newTurn;
        turn &= 0x03;
        invTurn = (turn + 2) & 0x03;
        turnDelegate(turn);
    }

    public void SetPos(float x, float y)
    {
        pos.x = x;
        pos.y = y;
        lastPos = pos;
        TurnTo(0);
    }

    public void SetClamp(float clampX, float clampY)
    {
        this.clampX = clampX;
        this.clampY = clampY;
    }
    

}//class
