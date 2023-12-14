using System;
using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float _speed;
    private bool _moving;
    private Vector2 _input;
    [NonSerialized] 
    public bool Running;

    public Movement(bool running)
    {
        Running = running;
    }

    private void Start()
    {
        _speed = Running ? 10 : 5;
    }

    private void Update()
    {
        if (!_moving)
        {
            _input.x = Input.GetAxisRaw("Horizontal");
            _input.y = Input.GetAxisRaw("Vertical");

            if (_input != Vector2.zero)
            {
                var targetPosition = transform.position;
                targetPosition.x += _input.x;
                targetPosition.y += _input.y;

                StartCoroutine(Move(targetPosition));
            }
        }
    }

    private IEnumerator Move(Vector3 targetPosition)
    {
        _moving = true;
        while ((targetPosition - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;

        _moving = false;
    }
}
