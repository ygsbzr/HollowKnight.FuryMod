using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modding;
using Vasi;
using UnityEngine;
using System.Reflection;
using GlobalEnums;

namespace Fury
{
    public class Fury : Mod, ITogglableMod
    {
        public override string GetVersion()
        {
            return "2.0.0.3Beta(V1.5.78)";
        }
        public override void Initialize()
        {
            ModHooks.CharmUpdateHook += CheckFury;
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += ChangeScene;
        }

        private void ChangeScene(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.Scene arg1)
        {
            if (global::PlayerData.instance.GetBool("equippedCharm_6"))
            {
                if(PlayerData.instance.equippedCharm_27)
                {
                    PlayerData.instance.joniHealthBlue = 1;
                }
                else
                {
                    global::PlayerData.instance.health = 1;
                    global::PlayerData.instance.maxHealth = 1;
                }
                GameObject furyCharm = GameObject.Find("Charm Effects");
                if (furyCharm != null)
                {
                    PlayMakerFSM FuryFsm = furyCharm.LocateMyFSM("Fury");
                    FuryFsm.ChangeTransition("Activate", "HERO HEALED FULL", "Stay Furied");
                    FuryFsm.ChangeTransition("Stay Furied", "HERO HEALED FULL", "Activate");
                    FuryFsm.Fsm.SendEventToFsmOnGameObject(furyCharm, "Fury", "HERO DAMAGED");
                    Log("Activate Fury Now!");
                }
            }
        }

        private void CheckFury(PlayerData data, HeroController controller)
        {
            if (global::PlayerData.instance.GetBool("equippedCharm_6"))
            {
                if(PlayerData.instance.equippedCharm_27)
                {
                    PlayerData.instance.joniHealthBlue = 1;
                }
                else
                {
                    global::PlayerData.instance.health = 1;
                    global::PlayerData.instance.maxHealth = 1;
                }
                GameObject furyCharm = GameObject.Find("Charm Effects");
                if (furyCharm != null)
                {
                    PlayMakerFSM FuryFsm = furyCharm.LocateMyFSM("Fury");
                    FuryFsm.ChangeTransition("Activate", "HERO HEALED FULL", "Stay Furied");
                    FuryFsm.ChangeTransition("Stay Furied", "HERO HEALED FULL", "Activate");
                    FuryFsm.Fsm.SendEventToFsmOnGameObject(furyCharm, "Fury", "HERO DAMAGED");
                    Log("Activate Fury Now!");
                }
            }
            if (!global::PlayerData.instance.GetBool("equippedCharm_6"))
            {
                GameObject furyCharm = GameObject.Find("Charm Effects");
                if (furyCharm != null)
                {
                    PlayMakerFSM FuryFsm = furyCharm.LocateMyFSM("Fury");
                    FuryFsm.ChangeTransition("Activate", "HERO HEALED FULL", "Deactivate");
                    FuryFsm.ChangeTransition("Stay Furied", "HERO HEALED FULL", "Deactivate");
                    FuryFsm.Fsm.SendEventToFsmOnGameObject(furyCharm, "Fury", "HERO HEALED");
                    Log("Deactive Fury");
                }
            }
        }

        public void Unload()
        {
            ModHooks.CharmUpdateHook -= CheckFury;
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged -= ChangeScene;
        }
    }
}
