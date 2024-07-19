using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;

// IEnumerable<RoomInfo>�C���^�[�t�F�[�X���������āAforeach�Ń��[������񋓂ł���悤�ɂ���
public class RoomList : IEnumerable<RoomInfo>
{
    private Dictionary<string, RoomInfo> dictionary = new Dictionary<string, RoomInfo>();

    public void Update(List<RoomInfo> changedRoomList)
    {
        foreach (var info in changedRoomList)
        {
            if (!info.RemovedFromList)
            {
                dictionary[info.Name] = info;
            }
            else
            {
                dictionary.Remove(info.Name);
            }
        }
    }

    public void Clear()
    {
        dictionary.Clear();
    }

    // �w�肵�����[�����̃��[����񂪂���Ύ擾����
    public bool TryGetRoomInfo(string roomName, out RoomInfo roomInfo)
    {
        return dictionary.TryGetValue(roomName, out roomInfo);
    }

    public IEnumerator<RoomInfo> GetEnumerator()
    {
        foreach (var kvp in dictionary)
        {
            yield return kvp.Value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
