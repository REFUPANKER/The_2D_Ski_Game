# Ski Game 
_[by 27-45 / LostMoon Games]_

<div id="home"></div>

## Documentation
- [Player](#player)
- [Ground Checker](#groundchecker)
- [Hearts Controller](#heartscontroller)
- [Panoramic Background](#panorama)
- [Checkpoints System](#checkpoints)
- [Random ground generating](#groundgenerater)

<div id="player">
<h3>Player</h3>
<h4 id="movement">Movement</h4>
    uses rigidbody and adds force to move player smoothly

    if (Input.GetKey(KeyCode.D)){
    rb.AddForce(this.transform.right * moveSpeed);}
<h4 id="backnfrontflip">Back & Front Flip</h4>
    
    if (Input.GetKey(KeyCode.S)){
    //backflip
        transform.Rotate(Vector3.forward * rotationForce * Time.deltaTime);
        flipTime += 1;
    }
    if (Input.GetKey(KeyCode.W)){
        //front flip
        transform.Rotate(-Vector3.forward * rotationForce * Time.deltaTime);
        flipTime += 1;}
<h4 id="camerascale">Camera Scale</h4>
    uses <a href="#groundchecker">GroundChecker</a><br>throws laser and calculates player distance to ground
    and changes field of view ;
    
    calcFov.orthographicSize = Mathf.Lerp(calcFov.orthographicSize, 
    Mathf.Clamp(gc.distanceToGround, 5f, MaxCameraFov), 
    Time.deltaTime * 5f);

<img src="https://github.com/REFUPANKER/The_2D_Ski_Game/assets/68808212/8eeb9ba8-bbb4-4afe-b8e6-3047a68d228a" width="320" height="210"/>
<h4 id="cameraalignment">Align Camera</h4>
    Camera follows user as smoothly

    transform.position = Vector3.Lerp(transform.position, 
    new Vector3(target.position.x + CenterDistance, 
    target.position.y, transform.position.z), 
    smoothDelay);
<h4 id="playeralignment">Align player to ground</h4>
    uses <a href="#groundchecker">GroundChecker</a><br>

    transform.rotation = Quaternion.Lerp(transform.rotation,
    Quaternion.FromToRotation(Vector3.up * Time.deltaTime, gc.rotater.normal), 
    3f * Time.deltaTime);

<img src="https://github.com/REFUPANKER/The_2D_Ski_Game/assets/68808212/552586ee-ce39-4c61-9137-33a4fac4b042" width="320" height="210"/>
<h4 id="headhit">Head Controller</h4>
    with unity api,we can detect collisions between objects.<br>
    we added CircleCollider to player 's head and used "OnCollisionEnter2D" method to detect collisions<br>
    playerHead might collide with player's body so we must check the collided object is ground layer

    public bool hit { get; set; } // notify other objects
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))// check the layer is "ground"
        {
            hit = true;
        }
    }
<h4 id="dashcontroller">Dash Controller</h4>
the speed boost for player , uses "E" key to activate dash for limited seconds
coroutines used for refill dash bar

        if (CanDash)
        {
            StartCoroutine("UseDash");
            CanDash = false;
            for (int i = 0; i < 100; i++)
            {
                rb.AddForce(player.right * dashPower);
            }
            StartCoroutine("CountDown");
        }
<a href="#home">Back to top</a>
</div>

<div id="timer">
<h3>Timer</h3>
<h4 id="basics">How it works</h4>
the basic timer,increases time value(miliseconds) and converts to normal time for user interface<br>

    IEnumerator timerCounter()
    {
        if (player.CanMove)
        {
            time += 1;
            label.text = GetAsUiTimeFormat();
        }
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(timerCounter());
    }
    public string GetAsUiTimeFormat(){
        return FillZero((time / (60 * 60)) % 60) + ":" + 
        FillZero((time / (60)) % 60) + ":" + 
        FillZero(time % 100);
    }
    private string FillZero(int num){
        return num < 10 ? "0" + num : num + "";
    }
<a href="#home">Back to top</a>
</div>

<div id="groundchecker">
<h3>Ground Checker</h3>
<h4 id="basics">How it works</h4>
throws an laser from specified point to distance then returns objects by layer<br>
displays distance as meter<br>
    
    RaycastHit2D hit = Physics2D.Raycast(
    transform.position, // raycast start point
    Vector2.down,       // the direction
    Mathf.Infinity,     // max distance
    layer);             // layer (in this case : "ground")
<img src="https://github.com/REFUPANKER/The_2D_Ski_Game/assets/68808212/f124848f-f518-4f60-a25c-f48260419916" width="320" height="210"/>

<a href="#home">Back to top</a>   
</div>

<div id="heartscontroller">
<h3>Hearths Controller</h3>
<h4 id="basics">How it works</h4>
gets heart objects from user interface<br>
in every start,refreshes hearts<br>
responsible with GameOver Screen<br>
plays animations and sounds for gameover screen<br>
<a href="#player">Player</a> uses hearthsController

    public void RemoveHeart()
    {
        if (getActiveHearts() <= 0)
        {
            player.CanMove = false;
            gameOverScreen.SetActive(true);
        }
        else
        {
            hitSfx.Stop();
            hitSfx.Play();
            Animation anim = getNextActiveHeart().GetComponent<Animation>();
            anim.Play();
            StartCoroutine(
            waitToanimate(anim, () =>
            {
                getNextActiveHeart().gameObject.SetActive(false);
            }));
        }
    }
<a href="#home">Back to top</a>
</div>


<div id="panorama">
<h3>Panorama</h3>
<h4 id="basics">How it works</h4>
panoramic view that adds more depth to game<br>
made with 3 layer,moves with opposite velocity of player<br>
the parent object holds instances of layers in horizontal direction<br>
layers moves by distance scale to player <br>
according to this formule : PlayerVelocity/DistanceScale<br>
uses <a href="#player">Player</a> to get player speed

     foreach (Transform item in this.transform)
     {
         item.Translate(-(velocity.velocity.x * Time.deltaTime / layerDistance), 0, 0);
     }
<img src="https://github.com/REFUPANKER/The_2D_Ski_Game/assets/68808212/8fc221e3-e0ae-4407-9eef-61b8902e005f" width="320" height="210"/>

<a href="#home">Back to top</a>
</div>

<div id="checkpoints">
<h3>Checkpoints Controller</h3>
<h4 id="basics">How it works</h4>
requires : instance of UI Checkpoint and InGame Checkpoints(Array)<br>
uses start and end point to calculate total distance <br>
calculates every ingame checkpoint object's distance to finish line <br>
then converts to UI values<br>

        foreach (var item in CheckPoints)
        {
            float InWorldPercent = 100 * Vector2.Distance(item.transform.position, StartPoint.position) / finishDistance;
            Transform NewUiCheckPoint = Instantiate(UiCheckpointInstance, UiCheckpointHolder);
            float inUiPointX = UiDistanceSlider.rect.width * (InWorldPercent / 100);
            NewUiCheckPoint.localPosition = new Vector2(inUiPointX, UiCheckpointInstance.position.y);
        }
<img src="https://github.com/REFUPANKER/The_2D_Ski_Game/assets/68808212/116f6a86-29ec-4e15-a5f2-c0eb2ae8fb3f" width="320" height="210"/>

<a href="#home">Back to top</a>
</div>

<div id="groundgenerater">
<h3>Ground Generater</h3>
<h4 id="basics">How it works</h4>
gets variables : width (points count) , points distance , points curve<br>
and converts to path for sprite renderer<br>
then sprite renderer fills the shape that created with points<br>
<img src="https://github.com/REFUPANKER/The_2D_Ski_Game/assets/68808212/55165270-cc99-4401-9cac-ff63ea1ef21d" width="320" height="210"/>

    public void ReStart(bool recheckpoint = false)
    {
        // clear previous objects
        ssc = this.GetComponent<SpriteShapeController>();
        ssc.spline.Clear();
        foreach (Transform item in CheckpointsHolder)
        {
            Destroy(item.gameObject);
        }

        UiController.CheckPoints.Clear();

        // add flat starting position for better landing
        ssc.spline.InsertPointAt(0, Vector3.zero);
        ssc.spline.InsertPointAt(1, new Vector3(HorizontalDistance, 0, 0));
        AddCheckpoint(HorizontalDistance / 2, 0);

        float l = HorizontalDistance * 2;
        float minY = ssc.spline.GetPosition(0).y;
        for (int i = 2; i <= Width; i++)
        {
            // calculate new Y point with previous Y point
            float preY = ssc.spline.GetPosition(i - 1).y;
            float t = Random.Range(preY - VerticalDistance, preY + VerticalDistance);
            minY = (t < minY) ? t : minY;
            ssc.spline.InsertPointAt(i, new Vector2(l, t));
            l += HorizontalDistance;
            if (i % perDistance == 0)
            {
                AddCheckpoint(ssc.spline.GetPosition(i).x, ssc.spline.GetPosition(i).y);
            }
        }

        // turn back to first point with fill
        Vector2 firstpoint = ssc.spline.GetPosition(0);
        Vector2 lastpoint = ssc.spline.GetPosition(ssc.spline.GetPointCount() - 1);
        ssc.spline.InsertPointAt(ssc.spline.GetPointCount(), new Vector2(lastpoint.x, minY - GroundFillDistance));
        ssc.spline.InsertPointAt(0, new Vector2(firstpoint.x, minY - GroundFillDistance));
        ssc.spline.InsertPointAt(ssc.spline.GetPointCount() - 1, new Vector2(finishLine.position.x - transform.position.x, finishLine.position.y - transform.position.y));
        ssc.spline.InsertPointAt(ssc.spline.GetPointCount() - 1, new Vector2(finishLine.position.x - transform.position.x - HorizontalDistance * 2, finishLine.position.y - transform.position.y));

        // adds curve between to points for better hill view
        for (int i = 0; i < ssc.spline.GetPointCount(); i++)
        {
            ssc.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            ssc.spline.SetLeftTangent(i, new Vector3(-PointCurve, 0, 0));
            ssc.spline.SetRightTangent(i, new Vector3(PointCurve, 0, 0));
        }
        
        // update user interface checkpoints
        if (recheckpoint)
        {
            UiController.ReplaceCheckpoints();
        }
    }
<a href="#home">Back to top</a>
</div>

