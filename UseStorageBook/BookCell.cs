using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GuiBaseUI;

namespace Sth4nothing.UseStorageBook
{
    public class BookCell : ItemCell
    {

        public ChildData[] childDatas;
        public override void Awake()
        {
            base.Awake();
            childDatas = new ChildData[NewBookView.columns];
            for (int i = 0; i < NewBookView.columns; i++)
            {
                Transform child = transform.GetChild(i);
                childDatas[i] = new ChildData(child);
            }
            //Main.Logger.Log("WarehouseItem Awake " + childDatas.Length);
        }
    }

    public class SetBook : MonoBehaviour
    {
        public void SetBookId(int bookId)
        {
            if (gameObject.name == "Item," + bookId)
                return;
            gameObject.name = "Item," + bookId;
            gameObject.GetComponent<Toggle>().group = HomeSystem.instance.bookHolder.GetComponent<ToggleGroup>();
            Image component = gameObject.transform.Find("ItemBack").GetComponent<Image>();
            component.sprite = GetSprites.instance.itemBackSprites[int.Parse(DateFile.instance.GetItemDate(bookId, 4, true))];
            component.color = ActorMenu.instance.LevelColor(int.Parse(DateFile.instance.GetItemDate(bookId, 8, true)));
            GameObject gameObject2 = gameObject.transform.GetChild(2).gameObject;
            gameObject2.name = "ItemIcon," + bookId;
            gameObject2.GetComponent<Image>().sprite = GetSprites.instance.itemSprites[int.Parse(DateFile.instance.GetItemDate(bookId, 98, true))];
            int num2 = int.Parse(DateFile.instance.GetItemDate(bookId, 901, true));
            int num3 = int.Parse(DateFile.instance.GetItemDate(bookId, 902, true));
            gameObject.transform.Find("ItemHpText").GetComponent<Text>().text = $"{ActorMenu.instance.Color3(num2, num3)}{num2}</color>/{num3}";
            int[] bookPage = DateFile.instance.GetBookPage(bookId);
            Transform transform = gameObject.transform.Find("PageBack");
            for (int j = 0; j < transform.childCount; j++)
            {
                if (bookPage[j] == 1)
                {
                    transform.GetChild(j).GetComponent<Image>().color = new Color(100f / 255, 100f / 255, 0f, 1f);
                }
            }
        }
        static void Travel(Transform obj, int level)
        {
            var indent = "";
            for (int i = 0; i < level; i++)
            {
                indent += '-';
            }
            Debug.Log($"{indent}{obj.name}");
            foreach (Transform child in obj)
            {
                Travel(child, level + 1);
            }
        }
    }
    public struct ChildData
    {
        public GameObject gameObject;
        public SetBook setBook;

        public ChildData(Transform child)
        {
            gameObject = child.gameObject;
            setBook = gameObject.AddComponent<SetBook>();
        }
    }
}