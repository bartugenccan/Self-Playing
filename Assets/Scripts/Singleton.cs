using System;

public class Singleton
{

    public bool EnemyState;

    // Event Handler
    public event EventHandler<EnemyDiedArgs> EnemyHandler;

    //Private instance object
    private static readonly Singleton instance = new Singleton();

    private Singleton() { }

    public static Singleton Instance
    {
        get
        {
            return instance;
        }
    }

    public void EnemyDied(EnemyDiedArgs e)
    {
        EventHandler<EnemyDiedArgs> handler = EnemyHandler;

        if (handler != null)
            handler(this, e);
    }
}

public class EnemyDiedArgs : EventArgs 
{
    public bool IsEnemyAlive
    {
        get; set;
    }
}