using UnityEngine;

public class NotesTable : MonoBehaviour
{
    [SerializeField] private Draggable[] _allNotes;
    [SerializeField] private GameObject _notesCameraPosition;


    private void Start()
    {
        CameraMovmentController.Instance.OnCameraMove.AddListener(CheakNotes);
    }
    private void CheakNotes()
    {
        if(CameraMovmentController.Instance.CurrentCameraPosition == _notesCameraPosition)
        {
            foreach (var note in _allNotes)
            {
                note.SetInteractable(true);
            }
        }
        else
        {
            foreach (var note in _allNotes)
            {
                note.SetInteractable(false);
            }
        }
    }
    public void SetAllNotesInteractable(bool isInteractable)
    {
        foreach (var note in _allNotes)
        {
            note.SetInteractable(isInteractable);
        }
    }
}
