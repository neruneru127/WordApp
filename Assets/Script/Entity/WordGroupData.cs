using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGroupData
{
    // primary key
    public string GroupName
    {
        get; set;
    }

    public bool PassedTestFlg
    {
        get; set;
    }

    public List<int> WordList
    {
        get; set;
    }

    
    public override bool Equals(object obj)
    {
        if (obj == null || this.GetType() != obj.GetType())
        {
            return false;
        }

        WordGroupData c = (WordGroupData)obj;
        return (this.GroupName == c.GroupName);
    }

    //Equals‚ªtrue‚ð•Ô‚·‚Æ‚«‚É“¯‚¶’l‚ð•Ô‚·
    public override int GetHashCode()
    {
        return this.GroupName.GetHashCode();
    }
}
