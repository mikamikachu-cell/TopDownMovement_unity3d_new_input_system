using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Ruler_new_input_system_better : MonoBehaviour
{
    [SerializeField] private float speed = 5f;//vitesse du joueur
    private ControllerInputActions playerInput;
    private Rigidbody2D rb;//rigidbody
    Animator animator;//animator pour jouer des animations

    public GameObject cercleOrange; // si le cercle orange est visible
   
    //Booleens
    bool hautAppuye = false, basAppuye = false, droiteAppuye = false, gaucheAppuye = false;//appuyé
    bool hautRelache = false, basRelache = false, droiteRelache = false, gaucheRelache = false;//relâché
    
      void Start(){
        animator = GetComponent<Animator>();
        animator.Play("ruler_idle_dos");
    }
    
    void Awake(){
        playerInput = new ControllerInputActions();
        rb = GetComponent<Rigidbody2D>();
        InitAction();
        
    }
    private void OnEnable(){ playerInput.Enable();}
    private void OnDisable(){ playerInput.Disable();}

    public void FixedUpdate(){
        Vector2 moveInput = playerInput.Movement.MoveUP.ReadValue<Vector2>();
        Vector2 moveInput2 = playerInput.Movement.MoveDown.ReadValue<Vector2>();
        Vector2 moveInput3 = playerInput.Movement.MoveRight.ReadValue<Vector2>();
        Vector2 moveInput4 = playerInput.Movement.MoveLeft.ReadValue<Vector2>();

        rb.velocity = moveInput * speed + moveInput2 * speed+moveInput3 * speed+moveInput4 * speed;
    }
    public void InitAction(){
        // UP
        
        playerInput.Movement.MoveUP.canceled += Movement_Up_canceled;
        //DOWN
        playerInput.Movement.MoveDown.canceled += Movement_Down_canceled;
        //LEFT
        playerInput.Movement.MoveRight.canceled +=  Movement_Right_canceled;
        //RIGHT
        playerInput.Movement.MoveLeft.canceled +=  Movement_Left_canceled;
        //Acceleration
        playerInput.Movement.accelerate.performed += Movement_accelerate_performed;
        playerInput.Movement.accelerate.canceled += Movement_accelerate_canceled;
    }
//ACCELERATE
public void Movement_accelerate_performed(InputAction.CallbackContext context){
      cercleOrange.GetComponent<Renderer>().enabled = true;//rond orange est visible 
      speed = 6f;
   } 

public void Movement_accelerate_canceled(InputAction.CallbackContext context){
        cercleOrange.GetComponent<Renderer>().enabled = false;//rond orange invisible
         speed = 5f;   
   }
//HAUT
  public void Movement_Up_canceled(InputAction.CallbackContext context){
        hautRelache = true;
    }
//DROITE
public void Movement_Right_canceled(InputAction.CallbackContext context){
     droiteRelache = true; 
}
//BAS
public void Movement_Down_canceled(InputAction.CallbackContext context){
            basRelache =true;
}
//GAUCHE
public void Movement_Left_canceled(InputAction.CallbackContext context){
    gaucheRelache = true;
    animator.Play("ruler_idle_droite");//idle gauche
}

 void Update(){
     
     if(rb.velocity.x >0 && rb.velocity.y <0){//face gauche
        if(cercleOrange.GetComponent<Renderer>().isVisible == false ){
        animator.Play("ruler_course_avant_gauche"); 
        }
        if(cercleOrange.GetComponent<Renderer>().isVisible == true ){
            animator.Play("ruler_sprint_face_gauche"); 
        }
     }
     else if(rb.velocity.x >0 && rb.velocity.y >0){//dos gauche
        print("gauche_dos");
        if(cercleOrange.GetComponent<Renderer>().isVisible == false ){
            animator.Play("ruler_course_arriere_gauche"); 
        }
        if(cercleOrange.GetComponent<Renderer>().isVisible == true ){
            animator.Play("ruler_sprint_dos_gauche"); 
        }
     }
     else if (rb.velocity.x <0 && rb.velocity.y <0){//face droit
        print("droite face");
         if(cercleOrange.GetComponent<Renderer>().isVisible == false ){
        animator.Play("ruler_course_avant_droit");
        }
        if(cercleOrange.GetComponent<Renderer>().isVisible == true ){
            animator.Play("ruler_sprint_face_droite"); 
        }
     }
     else if (rb.velocity.x <0 && rb.velocity.y>0){//dos droit
        print("droite dos"); 
         if(cercleOrange.GetComponent<Renderer>().isVisible == false ){
        animator.Play("ruler_course_arriere_droite"); 
        
        }
        if(cercleOrange.GetComponent<Renderer>().isVisible == true ){
            animator.Play("ruler_sprint_dos_droite"); 
        }
     }
        
      //directions
     else  if (rb.velocity.x < 0){
             droiteAppuye = true;  
            //Debug.Log("va à droite");//droite pour le joueur
        if(cercleOrange.GetComponent<Renderer>().isVisible == false){//si le cercle orange invisible
                animator.Play("ruler_course_vers_droite");//jouer course vers droite
        }
        else if(cercleOrange.GetComponent<Renderer>().isVisible == true){//si le cercle orange visible
            animator.Play("ruler_sprint_droite");
        }}
        else if(rb.velocity.x > 0){
            //Debug.Log("va à gauche");//gauche pour le joueur
             gaucheAppuye = true;
             if (cercleOrange.GetComponent<Renderer>().isVisible == false){//si le cercle orange est invisible
             animator.Play("ruler_course_vers_gauche");//jouer course vers droite 
        }
        else if(cercleOrange.GetComponent<Renderer>().isVisible == true){//si le cercle orange est visible
        animator.Play("ruler_sprint_sans_balle_gauche");
        }}
         else if (rb.velocity.y < 0){
            basAppuye = true;
              if(cercleOrange.GetComponent<Renderer>().isVisible == true){
                animator.Play("ruler_sprint_face"); 
             }
            else if(cercleOrange.GetComponent<Renderer>().isVisible == false){//si le cercle orange visible
            animator.Play("ruler_course_face");//course avant
             }}
        else if(rb.velocity.y > 0){
            //course sans balle dos
            if(cercleOrange.GetComponent<Renderer>().isVisible == false){//si le cercle orange est invisible
            Debug.Log("rond orange invisible");
            animator.Play("ruler_course_dos");//course_arriere  
            hautAppuye =true;
        }
         else if(cercleOrange.GetComponent<Renderer>().isVisible == true){//si le cercle orange est visible
            animator.Play("ruler_sprint_dos");
            
        } }
         //--------------------- Pressed------------------------------
        else if(hautAppuye && droiteAppuye){  //haut et droite appuyés en même temps
           if ((cercleOrange.GetComponent<Renderer>().isVisible == false)){
            animator.Play("ruler_course_arriere_droite");
           }   
           else if((cercleOrange.GetComponent<Renderer>().isVisible == true)){
            animator.Play("ruler_sprint_dos_droite");
           }
                hautAppuye =false ; 
                droiteAppuye = false ;}
        else if(hautAppuye  && gaucheAppuye){//haut et gauche appuyés en même temps
            if(cercleOrange.GetComponent<Renderer>().isVisible == false){//si le cercle orange visible
             animator.Play("ruler_course_arriere_gauche");//animation avant droit
            }
           else if(cercleOrange.GetComponent<Renderer>().isVisible == true){
                animator.Play("ruler_sprint_dos_gauche");
            }
               hautAppuye = false; 
               gaucheAppuye =false; 
        }
       else if(basAppuye && droiteAppuye){//bas et droit appuyés en même temps
                if(cercleOrange.GetComponent<Renderer>().isVisible == false){//si le cercle orange invisible
                    animator.Play("ruler_course_avant_gauche");
                }
               else if(cercleOrange.GetComponent<Renderer>().isVisible == true){//si le cercle orange est visible
                    animator.Play("ruler_sprint_face_gauche");
                }
            basAppuye = false ;
            droiteAppuye = false ;
        }
        else if(basAppuye && gaucheAppuye ){//bas et gauche appuyés en même temps
         if(cercleOrange.GetComponent<Renderer>().isVisible == false){//si le cercle orange invisible
            
            animator.Play("ruler_course_avant_droite");
             }
            else if(cercleOrange.GetComponent<Renderer>().isVisible == true){//si le cercle orange est visible
                animator.Play("ruler_sprint_face_droite");
             }
            basAppuye = false;
            gaucheAppuye = false;
        }
        //-------------------------------Release--------------------------------
        else if(hautRelache && droiteRelache && rb.velocity.x == 0 && rb.velocity.y == 0){ //haut et droit relachés
       
            animator.Play("ruler_idle_dos_gauche");
            hautRelache = false;
            droiteRelache = false; 
        }
        else if(hautRelache && gaucheRelache &&rb.velocity.x == 0 && rb.velocity.y == 0 ){
            animator.Play("ruler_idle_dos_droite");
            hautRelache = false;
            gaucheRelache = false; 
        }
       else if(basRelache && droiteRelache &&rb.velocity.x == 0 && rb.velocity.y == 0){//bas et droite relachés
            animator.Play("ruler_idle_face_gauche");
            basRelache =false;
            droiteRelache = false;
        }
        else if(basRelache && gaucheRelache && rb.velocity.x == 0 && rb.velocity.y == 0 ){//bas et gauche relachés
            animator.Play("ruler_idle_face_droite");
            basRelache = false;
            gaucheRelache = false;
        }
        else if (basRelache && rb.velocity.y == 0 && rb.velocity.x == 0){
                animator.Play("ruler_idle_face");//idle droit
                basRelache = false; 

        }
        else if(hautRelache  && rb.velocity.y == 0 && rb.velocity.x == 0){
             animator.Play("ruler_idle_dos");//idle dos
             hautRelache = false;
        }
        else if (droiteRelache && rb.velocity.y == 0 && rb.velocity.x == 0){
           animator.Play("ruler_idle_gauche");//idle gauche 
           droiteRelache = false;
        }
        else if (gaucheRelache && rb.velocity.y == 0 && rb.velocity.x == 0){
           animator.Play("ruler_idle_droite");//idle gauche 
           gaucheRelache = false; 
        }

        }
}

