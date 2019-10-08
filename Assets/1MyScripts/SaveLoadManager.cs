using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadManager {

    public static void SetEssence(int essence)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/essence.sav", FileMode.Create);

        EssenceData data = new EssenceData(essence);

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static void UpdateEssence(int essence)
    {
        if (File.Exists(Application.persistentDataPath + "/essence.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/essence.sav", FileMode.Open);

            EssenceData data = bf.Deserialize(stream) as EssenceData;

            stream.Close();

            FileStream stream2 = new FileStream(Application.persistentDataPath + "/essence.sav", FileMode.Create);

            EssenceData data2 = new EssenceData(essence);

            data2.UpdateTotalEssence(data.essence);

            bf.Serialize(stream2, data2);
            stream2.Close();
        }
        else
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/essence.sav", FileMode.Create);

            EssenceData data = new EssenceData(essence);

            bf.Serialize(stream, data);
            stream.Close();
        }
    }

    public static int getEssence()
    {
        if (File.Exists(Application.persistentDataPath + "/essence.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/essence.sav", FileMode.Open);

            EssenceData data = bf.Deserialize(stream) as EssenceData;

            stream.Close();
            return data.essence;
        }
        else
        {
            return 0;
        }
    }


    public static void SetHealthModifier(int health)
    {

        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/healthmodifier.sav", FileMode.Create);

        HealthModifierData data = new HealthModifierData(health);

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static void UpdateHealthModifier(int health)
    {
        if (File.Exists(Application.persistentDataPath + "/healthmodifier.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/healthmodifier.sav", FileMode.Open);

            HealthModifierData data = bf.Deserialize(stream) as HealthModifierData;
            stream.Close();

            FileStream stream2 = new FileStream(Application.persistentDataPath + "/healthmodifier.sav", FileMode.Create);

            HealthModifierData data2 = new HealthModifierData(health);

            data2.UpdateHealthModifier(data.HealthModifier);

            bf.Serialize(stream2, data2);
            stream2.Close();
        }
        else
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/healthmodifier.sav", FileMode.Create);

            HealthModifierData data = new HealthModifierData(health);

            bf.Serialize(stream, data);
            stream.Close();
        }
    }

    public static int getHealthModifier()
    {
        if (File.Exists(Application.persistentDataPath + "/healthmodifier.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/healthmodifier.sav", FileMode.Open);

            HealthModifierData data = bf.Deserialize(stream) as HealthModifierData;

            stream.Close();
            return data.HealthModifier;
        }
        else
        {
            return 1;
        }
    }


    public static void SetManaModifier(int mana)
    {

        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/manamodifier.sav", FileMode.Create);

        ManaModifierData data = new ManaModifierData(mana);

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static void UpdateManaModifier(int mana)
    {
        if (File.Exists(Application.persistentDataPath + "/manamodifier.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/manamodifier.sav", FileMode.Open);

            ManaModifierData data = bf.Deserialize(stream) as ManaModifierData;
            stream.Close();

            FileStream stream2 = new FileStream(Application.persistentDataPath + "/manamodifier.sav", FileMode.Create);

            ManaModifierData data2 = new ManaModifierData(mana);

            data2.UpdateManaModifier(data.ManaModifier);

            bf.Serialize(stream2, data2);
            stream2.Close();
        }
        else
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/manamodifier.sav", FileMode.Create);

            ManaModifierData data = new ManaModifierData(mana);

            bf.Serialize(stream, data);
            stream.Close();
        }

    }

    public static int getManaModifier()
    {
        if (File.Exists(Application.persistentDataPath + "/manamodifier.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/manamodifier.sav", FileMode.Open);

            ManaModifierData data = bf.Deserialize(stream) as ManaModifierData;

            stream.Close();
            return data.ManaModifier;
        }
        else
        {
            return 2;
        }
    }

    public static void SetDamageModifier(int damageModifier)
    {

        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/damagemodifier.sav", FileMode.Create);

        DamageModifierData data = new DamageModifierData(damageModifier);

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static void UpdateDamageModifier(int damageModifier)
    {
        if (File.Exists(Application.persistentDataPath + "/damagemodifier.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/damagemodifier.sav", FileMode.Open);

            DamageModifierData data = bf.Deserialize(stream) as DamageModifierData;
            stream.Close();

            FileStream stream2 = new FileStream(Application.persistentDataPath + "/damagemodifier.sav", FileMode.Create);

            DamageModifierData data2 = new DamageModifierData(damageModifier);

            data2.UpdateDamageModifier(data.damageModifier);

            bf.Serialize(stream2, data2);
            stream2.Close();
        }
        else
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/damagemodifier.sav", FileMode.Create);

            DamageModifierData data = new DamageModifierData(damageModifier);

            bf.Serialize(stream, data);
            stream.Close();
        }

    }

    public static int getDamageModifier()
    {
        if (File.Exists(Application.persistentDataPath + "/damagemodifier.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/damagemodifier.sav", FileMode.Open);

            DamageModifierData data = bf.Deserialize(stream) as DamageModifierData;

            stream.Close();
            return data.damageModifier;
        }
        else
        {
            return 0;
        }
    }

    public static void SetFightingStyle(int fightingStyle)
    {

        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/fightingstyle.sav", FileMode.Create);

        FightingStyleData data = new FightingStyleData(fightingStyle);

        bf.Serialize(stream, data);
        stream.Close();
    }   

    public static int getFightingStyle()
    {
        if (File.Exists(Application.persistentDataPath + "/fightingstyle.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/fightingstyle.sav", FileMode.Open);

            FightingStyleData data = bf.Deserialize(stream) as FightingStyleData;

            stream.Close();
            return data.fightingStyle;
        }
        else
        {
            return 0;
        }
    }


    public static void SetFirstRun(bool firstRun)
    {

        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/firstrun.sav", FileMode.Create);

        FirstRunData data = new FirstRunData(firstRun);

        bf.Serialize(stream, data);
        stream.Close();
    }  

    public static bool getFirstRun()
    {
        if (File.Exists(Application.persistentDataPath + "/firstrun.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/firstrun.sav", FileMode.Open);

            FirstRunData data = bf.Deserialize(stream) as FirstRunData;

            stream.Close();
            return data.firstRun;
        }
        else
        {
            return false;
        }
    }

}


[Serializable]
public class FirstRunData
{
    public bool firstRun = true;

    public FirstRunData(bool FirstRunVal)
    {
        firstRun = FirstRunVal;
    }

}

[Serializable]
public class FightingStyleData
{
    public int fightingStyle;

    public FightingStyleData(int fightingStyleVal)
    {
        fightingStyle = fightingStyleVal;
    }

}

[Serializable]
public class EssenceData
{
    public int essence;

    public EssenceData(int essenceVal)
    {
        essence = essenceVal;
    }

    public void UpdateTotalEssence(int essenceVal)
    {
        essence = essence + essenceVal;
    }
}

[Serializable]
public class HealthModifierData
{

    public int HealthModifier;

    public HealthModifierData(int health)
    {
        HealthModifier = health;
    }

    public void UpdateHealthModifier(int health)
    {
        HealthModifier = HealthModifier + health;
    }
}

[Serializable]
public class ManaModifierData
{

    public int ManaModifier;

    public ManaModifierData(int mana)
    {
        ManaModifier = mana;
    }

    public void UpdateManaModifier(int mana)
    {
        ManaModifier = ManaModifier + mana;
    }
}

[Serializable]
public class DamageModifierData
{

    public int damageModifier;

    public DamageModifierData(int damageModifierVal)
    {
        damageModifier = damageModifierVal;
    }

    public void UpdateDamageModifier(int damageModifierVal)
    {
        damageModifier = damageModifier + damageModifierVal;
    }
}

