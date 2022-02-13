using UnityEngine;

namespace LinkEmAll.Scripts
{
    public class Draw3D : MonoBehaviour {
        public GameObject rendererPrefab;
        private Camera _camera;
        private Plane _cast;
        private GameObject _currentRenderer;
        private Vector3 _origin;

        private void Start() {
            _camera = Camera.main;
            if (_camera != null) _cast = new Plane(_camera.transform.forward * -1, transform.position);
        }
    
        private void Update() {
            if (IsInput(TouchPhase.Began)) {
                var ray = _camera.ScreenPointToRay(Input.touchCount == 1
                    ? (Vector3) Input.GetTouch(0).position
                    : Input.mousePosition);

                if (!_cast.Raycast(ray, out var rayDistance)) return;
                _origin = ray.GetPoint(rayDistance);
                _currentRenderer = Instantiate(rendererPrefab, _origin, Quaternion.identity);
                
            } else if (IsInput(TouchPhase.Moved)) {
                var ray = _camera.ScreenPointToRay(Input.touchCount == 1
                    ? (Vector3) Input.GetTouch(0).position
                    : Input.mousePosition);

                if (!_cast.Raycast(ray, out var rayDistance)) return;
                _currentRenderer.transform.position = ray.GetPoint(rayDistance);

            } else if (IsInput(TouchPhase.Ended)) {
                if (Vector3.Distance(_currentRenderer.transform.position, _origin) < 0.1f)
                    Destroy(_currentRenderer);
            }
        }


        private bool IsInput(TouchPhase phase) {
            return phase switch {
                TouchPhase.Began => Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0),
                TouchPhase.Moved => Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetMouseButton(0),
                _ => Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0)
            };
        }
    }
}