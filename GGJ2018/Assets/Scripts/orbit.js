#pragma strict
public var center : Transform;
public var axis   : Vector3 = Vector3.up;
//public var radius;
//public var radiusSpeed = 0.5;
public var rotationSpeed = 6.0;

function Start() {
//	if (radius == 0) {
//		transform.position = (transform.position - center.position).normalized * radius + center.position;
//	}
}

function Update() {
    transform.RotateAround (center.position, axis, rotationSpeed * Time.deltaTime);
//    transform.RotateAround (transform, axis, radiusSpeed * Time.deltaTime);
}
