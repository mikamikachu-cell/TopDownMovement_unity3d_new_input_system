using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRunrtf4scene7 : MonoBehaviour
{
    public float moveSpeed = 5f;//vitesse
    public Rigidbody2D rb;
    Vector2 movement;//déplacement
    public Animator animator;//variable pour récupérer l'animator
    //variables de direction
    public float moveX ;//variable Horizontal
    public float moveY ;//variable vertical

//----------reconnaissance des directions
// threshold
    float threshold, threshold3 = 0.3f;
    float threshold2, threshold4 = -0.3f;
// prevframe
    float prevFrame, prevFrame2, prevFrame3, prevFrame4;
// currentframe
    float currentFrame, currentFrame2,currentFrame3,currentFrame4;
// //speed
    float speed, speed2, speed3, speed4 ; 
// relache
    bool relacheDroite, relacheGauche, relacheHaut, relacheBas; 

// --------VOID START----------------------------------------------------------------
    void Start(){
        animator.Play("rt4_idle_dos");
    }
//-------VOID UPDATE
    void Update()
    {
       
        //Mouvement de base
        moveX = movement.x = Input.GetAxisRaw("Horizontal");//mouvement horizontal
        moveY = movement.y = Input.GetAxisRaw("Vertical");//mouvement vertical


        //------------ ANIMATIONS EN FONCTION DES DIRECTIONS-----------------
        if (moveX == -1 && moveY == -1 ){
           // Debug.Log("joueur descend et va à droite");
            animator.Play("rt4_course_sans_balle_avant_droit");//animation avant droit
        }
        //-----------------------------------
        else if (moveX == 1 && moveY == -1 ){
           // Debug.Log("joueur descend et va à gauche");
            animator.Play("rt4_course_sans_balle_avant_gauche");//animation avant gauche
        }
        else if (moveX == -1 && moveY == 1 ){
          //  Debug.Log("joueur monte et va à droite");
            animator.Play("rt4_course_sans_balle_arriere_droite");//animation arriere droite
        }
         else if (moveX == 1 && moveY == 1 ){
          //  Debug.Log("joueur monte et va à gauche");
            animator.Play("rt4_course_sans_balle_arriere_gauche");//animation arriere gauche
          }
        
           //----------------ANIMATIONS EN FONCTION DES DIRECTIONS---------------------
        else if (moveX == -1 ){//personnage va a gauche
            animator.Play("rt4_course_sans_balle_vers_droite");//jouer course vers droite 
            Debug.Log("course_droite");

        }
       else if (moveX == 1){//personnage va a droite
            animator.Play("rt4_course_sans_balle_vers_gauche");//jouer course vers gauche  
            Debug.Log("course_gauche");

        }
       else if (moveY == 1 ){//personnage descend
            animator.Play("rt4_course_sans_balle_arriere");//course_arriere
            Debug.Log("course_arriere");

        }
        else if (moveY == -1){//personnage monte
            animator.Play("rt4_course_sans_balle_avant");//course avant
            Debug.Log("course_avant");
        }
       

        //---------------------TOUCHES RELACHEES-------------------------------
        else if(Input.GetKeyUp("up")){//touche haut
            animator.Play("rt4_idle_dos");//idle dos
        }
        else if(Input.GetKeyUp("down")){//touche bas
            animator.Play("rt4_idle_face");//idle face
        }
        else if(Input.GetKeyUp("right")){//touche droit
            animator.Play("rt4_idle_gauche");//idle droit
        }
        else if(Input.GetKeyUp("left")){//touche gauche
            animator.Play("rt4_idle_droite");//idle gauche
        }
               
    
    //----------------coté droit relaché 
         
         prevFrame = currentFrame; 
         currentFrame = moveX; 
         speed = currentFrame - prevFrame;
        if (speed > threshold)
        {
            relacheDroite = true;
        }
        if (relacheDroite)
        {
            animator.Play("rt4_idle_droite");//idle gauche
            relacheDroite = false;   
        } 
    //-----------Coté gauche relaché-------------
        prevFrame2 = currentFrame2; 
        currentFrame2 = moveX; 
        speed2 = currentFrame2 - prevFrame2;
        if (speed2 < threshold2)
        {
            relacheGauche = true;
        }
        if(relacheGauche)
        {
            animator.Play("rt4_idle_gauche");//idle droit
            relacheGauche = false;  
        }  
    //-----coté haut relaché---

        prevFrame3 = currentFrame3; 
        currentFrame3 = moveY; 
        speed3 = currentFrame3 - prevFrame3;
     if (speed3 > threshold3)
        {
            relacheBas = true;
        }
     if(relacheBas)
        {
            animator.Play("rt4_idle_face");//idle droit
            relacheBas = false;
         
        }
    //-----côté bas relaché---
         prevFrame4 = currentFrame4; 
         currentFrame4 = moveY; 
         speed4 = currentFrame4 - prevFrame4;
       
     if (speed4 < threshold4)
        {
            relacheHaut = true;
        }
    if(relacheHaut)
        {
            animator.Play("rt4_idle_dos");//idle droit
            relacheHaut = false;

        }
    //---------côté Bas droite
     if(speed4 < threshold4 && speed > threshold ){
        relacheBas =true;
        relacheDroite = true;
    }
     if(relacheBas && relacheDroite){
        relacheBas = false; 
        relacheDroite =false;
        animator.Play("rt4_idle_dos_droite");
      
    }
    // ---------côté Bas gauche
     if(speed4 < threshold4 && speed2 < threshold2){
        relacheBas = true;
        relacheGauche = true;
    }
     if(relacheBas && relacheGauche){
        relacheBas = false;
        relacheGauche = false;
        animator.Play("rt4_idle_dos_gauche");
        
    }
     //---------coté Haut droite
     if(speed3 > threshold3 && speed > threshold){
        relacheHaut = true;
        relacheDroite =true;
    }
     if(relacheHaut && relacheDroite){
        relacheDroite = false;
        relacheHaut = false;
        animator.Play("rt4_idle_face_droite");
       
    }
    // --------coté Haut gauche
     if(speed3 > threshold3 && speed2 < threshold2 ){
        relacheHaut   =  true;
        relacheGauche = true;
    }
     if (relacheHaut && relacheGauche){
        relacheHaut   =  false;
        relacheGauche = false;
        animator.Play("rt4_idle_face_gauche");   
    }   
    }
   
    //----------VOID FIXEDUPDATE
    void FixedUpdate(){
        rb.MovePosition(rb.position+movement*moveSpeed*Time.fixedDeltaTime);
    }
}
