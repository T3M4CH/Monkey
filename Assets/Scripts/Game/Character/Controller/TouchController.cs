using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Character.Controller
{
    public class TouchController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private Rigidbody characterRigidbody;
        [SerializeField] private CharacterRotation characterRotation;
        [SerializeField] private JumpController jumpController;
        [SerializeField] private GrabHolder grabHolder;
        
        private bool _dragging;


        public void OnBeginDrag(PointerEventData eventData)
        {
            jumpController.BeginStretch();
            characterRotation.BeginStretch();
        }

        public void OnDrag(PointerEventData eventData)
        {
            _dragging = true;
            jumpController.Stretch(eventData.delta.y / (canvas.scaleFactor * 175));
            characterRotation.Stretch(eventData.delta.x / (canvas.scaleFactor * 50));
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _dragging = false;
            if (!characterRigidbody.isKinematic) return;
            characterRigidbody.isKinematic = false;
            jumpController.AddForce();
            characterRotation.AddForce();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_dragging)
            {
                grabHolder.Grab();
            }
        }
    }
}