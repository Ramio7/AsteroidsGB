using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace RRRStudyProject.HW6
{
    public class TestMain : MonoBehaviour
    {
        private void Start()
        {
            Unit mage1 = new Mage(100);
            Unit infantry1 = new Infantry(150);
            Mage mage2 = new Mage(50);

            UnitRoot unitRoot = new UnitRoot();
            List<Unit> unitList = new List<Unit>
            {
                mage1,
                infantry1,
                mage2
            };

            unitRoot.unitArray = unitList.ToArray();

            var jSONData = new JSONData<UnitRoot>();
            jSONData.Save(unitRoot);
            string savePath = Path.Combine(jSONData.saveDirectoryPath, jSONData.saveFileName);
            UnitRoot units = jSONData.Load(savePath);
            for (int i = 0; i < units.unitArray.Length; i++)
            {
                Debug.Log($"{units.unitArray[i].Type}, {units.unitArray[i].Health}");
            }
        }
    }
}