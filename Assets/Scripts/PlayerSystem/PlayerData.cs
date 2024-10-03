using DebugLogic;
using UnityEngine;

namespace PlayerSystem
{
	[CreateAssetMenu(menuName = "ScriptableObjects/PlayerData", fileName = "PlayerData")]
	public class PlayerData : ScriptableObject
	{
		[Header("Gravity")]
		[ReadOnly] public float gravityStrength;
		[ReadOnly] public float gravityScale;

		[Space(5)]
		public float fallGravityMult;
		public float maxFallSpeed;

		[Space(5)]
		public float fastFallGravityMult;
		public float maxFastFallSpeed;

		[Space(20)]

		[Header("Run")]
		public float runMaxSpeed;

		public float runAcceleration;
		[ReadOnly] public float runAccelAmount;
		public float runDecceleration;
		[ReadOnly] public float runDeccelAmount;

		[Space(5)]
		[Range(0f, 1)] public float accelInAir;

		[Range(0f, 1)] public float deccelInAir;
		[Space(5)]
		public bool doConserveMomentum = true;

		[Space(20)]

		[Header("Jump")]
		public float jumpHeight;
		public float jumpTimeToApex; 
		[ReadOnly] public float jumpForce;

		[Header("Double Jump")]
		public float doubleJumpTimeToApex;
		[ReadOnly] public float doubleJumpForce;
		
		[Header("Both Jumps")]
		public float jumpCutGravityMult; 
		[Range(0f, 1)] public float jumpHangGravityMult;
		public float jumpHangTimeThreshold;
		[Space(0.5f)]
		public float jumpHangAccelerationMult; 
		public float jumpHangMaxSpeedMult; 				

		[Space(20)]

		[Header("Assists")]
		[Range(0.01f, 0.5f)] public float coyoteTime; 
		[Range(0.01f, 0.5f)] public float jumpInputBufferTime;
	

		private void OnValidate()
		{
			gravityStrength = -(2 * jumpHeight) / (jumpTimeToApex * jumpTimeToApex);
		
			gravityScale = gravityStrength / Physics2D.gravity.y;

			runAccelAmount = (50 * runAcceleration) / runMaxSpeed;
			runDeccelAmount = (50 * runDecceleration) / runMaxSpeed;

			jumpForce = Mathf.Abs(gravityStrength) * jumpTimeToApex;
			doubleJumpForce = Mathf.Abs(gravityStrength) * doubleJumpTimeToApex;

			#region Variable Ranges
			runAcceleration = Mathf.Clamp(runAcceleration, 0.01f, runMaxSpeed);
			runDecceleration = Mathf.Clamp(runDecceleration, 0.01f, runMaxSpeed);
			#endregion
		}
	}
}