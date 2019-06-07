using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBlock : MonoBehaviour
{
    public bool blockToBeDestroy = false; //if set to true, this block will not be scanned by other blocks so that we don't have an infinite loop running (y u running?)
    public bool drillThisBlock = false; //when the player will drill 1 block
    
    float posX;
    float posY;
    Vector2 downPoint, topPoint, leftPoint, rightPoint, rightStartingPoint, leftStartingPoint, topStartingPoint, downStartingPoint; //direction vectors

    // Update is called once per frame
    private void Update()
    {
        posX = transform.position.x;
        posY = transform.position.y;
        rightStartingPoint = new Vector2(posX + 0.76f, posY);
        leftStartingPoint = new Vector2(posX - 0.76f, posY);
        topStartingPoint = new Vector2(posX, posY + 0.76f);
        downStartingPoint = new Vector2(posX, posY - 0.76f);
        rightPoint = new Vector2(posX + 1.375f, posY);
        leftPoint = new Vector2(posX - 1.375f, posY);
        topPoint = new Vector2(posX, posY + 1.375f);
        downPoint = new Vector2(posX, posY - 1.375f);
        /*Debug.DrawLine(rightStartingPoint, rightPoint, Color.cyan);
        Debug.DrawLine(topStartingPoint, topPoint, Color.blue);
        Debug.DrawLine(leftStartingPoint, leftPoint, Color.green);
        Debug.DrawLine(downStartingPoint, downPoint, Color.red);*/
        if (drillThisBlock)
        {
            BlockDestruction();
        }
    }

    public void BlockDestruction()
    {
        blockToBeDestroy = true;
        /*BlockHitRight();
        BlockHitLeft();
        BlockHitTop();
        BlockHitDown();*/
        ABlockHit(topStartingPoint, topPoint);
        ABlockHit(rightStartingPoint, rightPoint);
        ABlockHit(downStartingPoint, downPoint);
        ABlockHit(leftStartingPoint, leftPoint);
        StartCoroutine(scoreAndBlockDestruction());
    }

    void ABlockHit(Vector2 aVector, Vector2 anotherVector)
    {
        RaycastHit2D blockHit = Physics2D.Linecast(aVector, anotherVector, 1 << LayerMask.NameToLayer("Blocks"));
        if (blockHit.collider != null)
        {
            if (blockHit.collider.gameObject.tag == this.gameObject.tag)
            {
                DestroyBlock otherBlock = blockHit.collider.gameObject.GetComponent<DestroyBlock>();
                if (!otherBlock.blockToBeDestroy)
                {
                    otherBlock.BlockDestruction();
                }
            }
        }
    }

    /*void BlockHitTop()
    {
        RaycastHit2D hitTop = Physics2D.Linecast(topStartingPoint, topPoint, 1 << LayerMask.NameToLayer("Blocks"));
        if (hitTop.collider != null)
        {
            if (hitTop.collider.gameObject.tag == this.gameObject.tag)
            {
                DestroyBlock otherBlock = hitTop.collider.gameObject.GetComponent<DestroyBlock>();
                if (!otherBlock.blockToBeDestroy)
                {
                    otherBlock.BlockDestruction();
                }
            }
        }
    }
    void BlockHitRight()
    {
        RaycastHit2D hitRight = Physics2D.Linecast(rightStartingPoint, rightPoint, 1 << LayerMask.NameToLayer("Blocks"));
        if (hitRight.collider != null)
        {
            if (hitRight.collider.gameObject.tag == this.gameObject.tag)
            {
                DestroyBlock otherBlock = hitRight.collider.gameObject.GetComponent<DestroyBlock>();
                if (!otherBlock.blockToBeDestroy)
                {
                    otherBlock.BlockDestruction();
                }
            }
        }
    }
    void BlockHitDown()
    {
        RaycastHit2D hitDown = Physics2D.Linecast(downStartingPoint, downPoint, 1 << LayerMask.NameToLayer("Blocks"));
        if (hitDown.collider != null)
        {
            if (hitDown.collider.gameObject.tag == this.gameObject.tag)
            {
                DestroyBlock otherBlock = hitDown.collider.gameObject.GetComponent<DestroyBlock>();
                if (!otherBlock.blockToBeDestroy)
                {
                    otherBlock.BlockDestruction();
                }
            }
        }
    }
    void BlockHitLeft()
    {
        RaycastHit2D hitLeft = Physics2D.Linecast(leftStartingPoint, leftPoint, 1 << LayerMask.NameToLayer("Blocks"));
        if (hitLeft.collider != null)
        {
            if (hitLeft.collider.gameObject.tag == this.gameObject.tag)
            {
                DestroyBlock otherBlock = hitLeft.collider.gameObject.GetComponent<DestroyBlock>();
                if (!otherBlock.blockToBeDestroy)
                {
                    otherBlock.BlockDestruction();
                }
            }
        }
    }*/
    public IEnumerator scoreAndBlockDestruction()
    {
        yield return new WaitForSeconds(0.3f);
        Score.score += 100;
        Object.Destroy(this.gameObject);
    }
}
