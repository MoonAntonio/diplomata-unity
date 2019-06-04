using System;
using System.Collections.Generic;
using LavaLeak.Diplomata.Dictionaries;
using LavaLeak.Diplomata.Helpers;
using LavaLeak.Diplomata.Models.Submodels;
using LavaLeak.Diplomata.Persistence;
using LavaLeak.Diplomata.Persistence.Models;
using UnityEngine;

namespace LavaLeak.Diplomata.Models
{
  /// <summary>
  /// A base class to talkable objects.
  /// </summary>
  [Serializable]
  public class Talkable : Data
  {
    [SerializeField]
    protected string uniqueId = Guid.NewGuid().ToString();

    public string Id
    {
      get { return uniqueId; }
    }

    public string name;
    public LanguageDictionary[] description;
    public Context[] contexts;

    [NonSerialized]
    public bool onScene;

    /// <summary>
    /// Basic constructor.
    /// </summary>
    /// <returns>The new Talkable.</returns>
    public Talkable() {}

    /// <summary>
    /// A talkable base constructor with name.
    /// </summary>
    /// <param name="name">The talkable name.</param>
    /// <param name="languages">All languages to set.</param>
    public Talkable(string name, Options options)
    {
      uniqueId = Guid.NewGuid().ToString();
      this.name = name;
      contexts = new Context[0];
      description = new LanguageDictionary[0];

      foreach (var lang in options.languages)
        description = ArrayHelper.Add(description, new LanguageDictionary(lang.name, ""));
    }

    /// <summary>
    /// Set the uniqueId if it is empty or null.
    /// </summary>
    /// <returns>Return true if it change or false if don't.</returns>
    public bool SetId()
    {
      if (!string.IsNullOrEmpty(uniqueId)) return false;
      uniqueId = Guid.NewGuid().ToString();
      return true;
    }
    
    /// <summary>
    /// Update the list of talkables in the DiplomataManager.Data.
    /// </summary>
    public static void UpdateList<TModel, TMono>(string path, ref List<TModel> talkables, ref Options options) where TModel : Talkable where TMono : DiplomataTalkable
    {
      var files = Resources.LoadAll(path);

      talkables = new List<TModel>();

      if (typeof(TModel) == typeof(Character))
        options.characterList = new string[0];
      if (typeof(TModel) == typeof(Interactable))
        options.interactableList = new string[0];

      foreach (var file in files)
      {
        var json = (TextAsset) file;
        var talkable = JsonUtility.FromJson<TModel>(json.text);

        talkables.Add(talkable);
        
        if (typeof(TModel) == typeof(Character))
          options.characterList = ArrayHelper.Add(options.characterList, file.name);

        if (typeof(TModel) == typeof(Interactable))
          options.interactableList = ArrayHelper.Add(options.interactableList, file.name);
      }

      var charactersOnScene = UnityEngine.Object.FindObjectsOfType<TMono>();

      foreach (var talkable in talkables)
      {
        foreach (var mono in charactersOnScene)
        {
          if (mono.talkable != null)
          {
            if (talkable.name == mono.talkable.name)
            {
              talkable.onScene = true;
            }
          }
        }
      }
    }

    /// <summary>
    /// Return the data of the object to save in a persistent object.
    /// </summary>
    /// <returns>A persistent object.</returns>
    public override Persistent GetData()
    {
      if (this.GetType() == typeof(Character))
      {
        var talkable = (Character) this;
        var talkablePersistent = new CharacterPersistent();
        talkablePersistent.id = uniqueId;
        talkablePersistent.influence = talkable.influence;
        talkablePersistent.contexts = Data.GetArrayData<ContextPersistent>(contexts);
        return talkablePersistent;
      }
      else if (this.GetType() == typeof(Interactable))
      {
        var talkable = (Interactable) this;
        var talkablePersistent = new InteractablePersistent();
        talkablePersistent.id = talkable.uniqueId;
        talkablePersistent.contexts = Data.GetArrayData<ContextPersistent>(talkable.contexts);
        return talkablePersistent;
      }
      else
      {
        var talkablePersistent = new TalkablePersistent();
        talkablePersistent.id = uniqueId;
        talkablePersistent.contexts = Data.GetArrayData<ContextPersistent>(contexts);
        return talkablePersistent;
      }
    }

    /// <summary>
    /// Store in a object data from persistent object.
    /// </summary>
    /// <param name="persistentData">The persistent data object.</param>
    public override void SetData(Persistent persistentData)
    {
      if (this.GetType() == typeof(Character))
      {
        var talkablePersistent = (CharacterPersistent) persistentData;
        uniqueId = talkablePersistent.id;
        ((Character) this).influence = talkablePersistent.influence;
        contexts = Data.SetArrayData<Context>(contexts, talkablePersistent.contexts);
      }
      else if (this.GetType() == typeof(Interactable))
      {
        var talkablePersistent = (InteractablePersistent) persistentData;
        uniqueId = talkablePersistent.id;
        contexts = Data.SetArrayData<Context>(contexts, talkablePersistent.contexts);
      }
      else
      {
        var talkablePersistent = (TalkablePersistent) persistentData;
        uniqueId = talkablePersistent.id;
        contexts = Data.SetArrayData<Context>(contexts, talkablePersistent.contexts);
      }
    }
  }
}
