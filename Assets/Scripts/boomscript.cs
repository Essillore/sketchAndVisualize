using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boomscript : MonoBehaviour
{
 
        private Animator animator;
        private bool hasPlayed = false;

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            // Check if the animation has not played yet
            if (!hasPlayed)
            {
                // Trigger the animation
                animator.SetTrigger("Death");

            // Set the flag to true to ensure it won't play again
            hasPlayed = true;


        }
        // Function to be called by the animation event
        void explosion()
        {
            Destroy(gameObject);
        }
    }
}
    