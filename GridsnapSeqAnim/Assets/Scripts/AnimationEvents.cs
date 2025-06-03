using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationEvents : MonoBehaviour
{
    public bool removingShapes = false;
    public void RemoveShapes(int shapesToRemove, params GroupAnimation[] groups)
    {
        removingShapes = true;
        foreach (GroupAnimation group in groups)
        {
            group.RemoveShapes(shapesToRemove);
        }
        removingShapes = false;
    }

    public void SetRotation(GroupAnimation group, Vector3 rotationSpeed, float smoothing, float stagger)
    {
        StartCoroutine(group.SetRotations(rotationSpeed, smoothing, stagger));
    }

    public void RandomRotation(GroupAnimation group)
    {
        Vector3 rotationSpeed = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
        float smoothing = Random.Range(.01f, 1);
        float stagger = Random.Range(0, 1);
        StartCoroutine(group.SetRotations(rotationSpeed, smoothing, stagger));
    }

    public void RandomRotation(GroupAnimation group, float smoothing, float stagger)
    {
        Vector3 rotationSpeed = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
        StartCoroutine(group.SetRotations(rotationSpeed, smoothing, stagger));
    }

    public void GroupRotation(float speed, params GroupAnimation[] groups)
    {
        foreach(GroupAnimation group in groups)
        {
            group.RotateGroup(speed);
        }
    }

    public void SetGroupRotationSpeed(float speed, params GroupAnimation[] groups)
    {
        foreach (GroupAnimation group in groups)
        {
            group.groupRotationSpeed = .35f;
        }
    }

    public void ArrangeInCircle(params GroupAnimation[] groups)
    {
        foreach (GroupAnimation group in groups)
        {
            ArrangeInCircle(group, group.circleRadius, group.arrangeSpeed);
        }
    }

    public void ArrangeInCircle(float radius, float speed, params GroupAnimation[] groups)
    {
        foreach (GroupAnimation group in groups)
        {
            ArrangeInCircle(group, radius, speed);
        }
    }

    public void ArrangeInCircle(GroupAnimation group, float radius, float speed)
    {
        group.circleRadius = radius;
        group.arrangeSpeed = speed;
        group.arrangeInCircle = true;
    }

    public IEnumerator WaitAndArrangeInCircle(float radius, float speed, float timeToWait, params GroupAnimation[] groups)
    {
        yield return new WaitForSeconds(timeToWait);
        foreach (GroupAnimation group in groups)
        {
            ArrangeInCircle(radius, speed, groups);
        }
    }

    public bool ArrangeInCircleStep(params GroupAnimation[] groups)
    {
        foreach (GroupAnimation group in groups)
        {
            ArrangeInCircleStep(group);
        }
        return true;
    }

    public bool ArrangeInCircleStep(GroupAnimation group)
    {
        return group.ArrangeInCircleStep(group.circleRadius, group.arrangeSpeed);
    }

    public bool ArrangeInCircleStep(GroupAnimation group, float radius, float speed)
    {
        return group.ArrangeInCircleStep(radius, speed);
    }

    public bool ArrangeInCircleStepRandom(params GroupAnimation[] groups)
    {
        foreach (GroupAnimation group in groups)
        {
            ArrangeInCircleStepRandom(group);
        }
        return true;
    }

    public bool ArrangeInCircleStepRandom(GroupAnimation group)
    {
        return ArrangeInCircleStepRandom(group, group.circleRadius, group.arrangeSpeed);
    }

    public bool ArrangeInCircleStepRandom(GroupAnimation group, float radius, float speed)
    {
        return group.ArrangeInCircleStepRandom(radius, speed);
    }

    public void ToggleCircleStepRandom(params GroupAnimation[] groups)
    {
        foreach (GroupAnimation group in groups)
        {
            group.ToggleCircleStepRandom(group.circleRadius, group.arrangeSpeed);
        }
    }

    public void ToggleCircleStepRandom(GroupAnimation group, float radius, float speed)
    {
        group.ToggleCircleStepRandom(radius, speed);
    }

    public void ArrangeInLargeCircle(float radius, float speed, params GroupAnimation[] groups)
    {
        StartCoroutine(DoArrangeInLargeCircle(radius, speed, groups));
    }

    private IEnumerator DoArrangeInLargeCircle(float radius, float speed, params GroupAnimation[] groups)
    {
        List<ShapeAnimation> shapes = new List<ShapeAnimation>();
        foreach (GroupAnimation group in groups)
        {
            shapes.AddRange(group.shapes);
            group.arrangeInUnison = false;
            group.arrangeInCircle = false;
        }
        for (int i = 0; i < shapes.Count; i++)
        {
            float angle = i * Mathf.PI * 2f / shapes.Count;
            Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
            shapes[i].StartPositionLerp(newPos, speed);
            yield return new WaitForSeconds(groups[0].staggerLength);
        }
        foreach (GroupAnimation group in groups)
        {
            group.arrangeInCircle = false;
        }
    }

    public void ArrangeInUnison(params GroupAnimation[] groups)
    {
        foreach(GroupAnimation group in groups)
        {
            ArrangeInUnison(group, group.arrangeSpeed);
        }
    }

    public void ArrangeInUnison(float speed, params GroupAnimation[] groups)
    {
        foreach (GroupAnimation group in groups)
        {
            ArrangeInUnison(group, speed);
        }
    }

    public void ArrangeInUnison(GroupAnimation group, float speed)
    {
        ArrangeInUnison(group, speed, group.staggerLength);
    }

    public void ArrangeInUnison(GroupAnimation group, float speed, float stagger)
    {
        group.staggerLength = stagger;
        group.arrangeSpeed = speed;
        group.arrangeInUnison = true;
    }

    public bool ArrangeInUnisonStep(params GroupAnimation[] groups)
    {
        foreach (GroupAnimation group in groups)
        {
            ArrangeInUnisonStep(group);
        }
        return true;
    }

    public bool ArrangeInUnisonStep(GroupAnimation group)
    {
        return group.ArrangeInUnisonStep(group.arrangeSpeed);
    }

    public bool ArrangeInUnisonStep(GroupAnimation group, float speed)
    {
        return group.ArrangeInUnisonStep(speed);
    }

    public void ToggleGroupRotation(bool startBackward, params GroupAnimation[] groups)
    {
        foreach (GroupAnimation group in groups)
        {
            ToggleGroupRotation(group, group.groupRotationSpeed, group.groupRotationSmoothing, startBackward);
        }
    }

    public void ToggleGroupRotation(GroupAnimation group, float speed, float smoothing, bool startBackward)
    {
        if (startBackward)
        {
            group.rotatingBackwards = true;
        }
        group.groupRotationSmoothing = smoothing;

        if (group.rotatingBackwards)
        {
            group.groupRotationSpeed = speed * -1;
            group.rotatingBackwards = false;
        }
        else
        {
            group.groupRotationSpeed = speed;
            group.rotatingBackwards = true;
        }
    }

    public void ToggleArrangement(params GroupAnimation[] groups)
    {
        foreach (GroupAnimation group in groups)
        {
            group.toggleArrangement = true;
        }
    }

    public void ToggleArrangement(GroupAnimation group)
    {
        group.toggleArrangement = true;
    }

    //public void ToggleCircleStep(bool startOutward, params GroupAnimation[] groups)
    //{
    //    foreach (GroupAnimation group in groups)
    //    {
    //        ToggleCircleStep(startOutward, group);
    //    }
    //}

    public void ToggleCircleStep(GroupAnimation group, bool startOutward)
    {
        ToggleCircleStep(group, group.circleRadius, group.arrangeSpeed, startOutward);
    }

    public void ToggleCircleStep(GroupAnimation group, float radius, float speed, bool startOutward)
    {
        if (startOutward)
        {
            group.arrangeInCircle = true;
            group.arrangingInward = true;
        }

        if (!group.arrangingInward)
        {
            if (!ArrangeInCircleStep(group, radius, speed))
            {
                group.arrangingInward = true;
            }
        }
        else
        {
            if (!ArrangeInCircleStep(group, 0, speed))
            {
                group.arrangingInward = false;
            }
        }
    }

    public void Resize(GroupAnimation group, float newSize)
    {
        Resize(group, newSize, group.resizeSpeed);
    }

    public void Resize(GroupAnimation group, float newSize, float smoothing)
    {
        group.resizeSpeed = smoothing;
        group.xSize = newSize;
        group.ySize = newSize;
        group.zSize = newSize;
    }

    public void ToggleGrowShrink(GroupAnimation group, float min, float max, float smoothing, bool startGrowing)
    {
        group.resizeSpeed = smoothing;
        if (startGrowing)
        {
            group.xSize = min;
            group.ySize = min;
            group.zSize = min;

            if (Mathf.Abs(transform.localScale.x - group.xSize) < .01f
                && Mathf.Abs(transform.localPosition.y - group.ySize) < .01f
                && Mathf.Abs(transform.localPosition.z - group.zSize) < .01f)
            {
                group.shrinking = false;
            }
        }

        if (group.shrinking)
        {
            group.xSize = min;
            group.ySize = min;
            group.zSize = min;
            if (Mathf.Abs(transform.localScale.x - group.xSize) < .01f
                && Mathf.Abs(transform.localPosition.y - group.ySize) < .01f
                && Mathf.Abs(transform.localPosition.z - group.zSize) < .01f)
            {
                group.shrinking = false;
            }
        }
        else
        {
            group.xSize = max;
            group.ySize = max;
            group.zSize = max;
            if (Mathf.Abs(transform.localScale.x - group.xSize) < .01f
                && Mathf.Abs(transform.localPosition.y - group.ySize) < .01f
                && Mathf.Abs(transform.localPosition.z - group.zSize) < .01f)
            {
                group.shrinking = true;
            }
        }
    }

    public void MoveTo(GroupAnimation group, Vector3 newPos)
    {
        MoveTo(group, group.positionSpeed, newPos);
    }

    public void MoveTo(GroupAnimation group, float speed, Vector3 newPos)
    {
        group.newPosition = newPos;
        group.setPosition = true;
    }

    public void ResetPositions(GroupAnimation group)
    {
        group.ResetShapePositions();
    }

    public void Glitch(GroupAnimation group, bool epilepsySafe)
    {
        if (!epilepsySafe)
            group.glitch = true;
    }

    public void GlitchWithColor(GroupAnimation group, bool epilepsySafe)
    {
        if (!epilepsySafe)
            group.glitchWithColor = true;
    }

    public void BounceToAndFro(GroupAnimation group, float speed, Vector3 pos1, Vector3 pos2)
    {
        group.positionSpeed = speed;
        if (group.switchPos)
        {
            group.newPosition = pos2;
            group.switchPos = false;
        }
        else
        {
            group.newPosition = pos1;
            group.switchPos = true;
        }
        group.setPosition = true;
    }

    public void TossUp(GroupAnimation group)
    {
        TossUp(group, group.force);
    }

    public void TossUp(GroupAnimation group, float force)
    {
        StartCoroutine(group.TossUp(force));
    }

    public void TossUpSingle(GroupAnimation group, float speed, int keyMin, int keyMax, int currKey)
    {
        group.TossUpSingle(speed, keyMin, keyMax, currKey);
    }

    public void TossUpSingleIncremental(GroupAnimation group)
    {
        group.TossUpSingleIncremental(group.force);
    }

    public void Explode(params GroupAnimation[] groups)
    {
        foreach (GroupAnimation group in groups)
        {
            Explode(group, group.force);
        }
    }
         
    public void Explode(GroupAnimation group)
    {
        Explode(group, group.force);
    }

    public void Explode(GroupAnimation group, float force)
    {
        group.Explode(force);
    }

    public void ToggleExplode(GroupAnimation group, float force)
    {

    }

    public void ResetSizes(GroupAnimation group)
    {
        group.resetSize = true;
    }

    public void Disappear(params GroupAnimation[] groups)
    {
        foreach(GroupAnimation group in groups)
        {
            group.Disappear(group.resizeSpeed);
        }
    }

    public void Disappear(GroupAnimation group, float speed)
    {
        group.Disappear(speed);
    }

    public void Reappear(params GroupAnimation[] groups)
    {
        foreach (GroupAnimation group in groups)
        {
            group.Reappear(group.resizeSpeed);
        }
    }

    public void Reappear(GroupAnimation group, float speed)
    {
        group.Reappear(speed);
    }

    public void ToggleUseGravity(params GroupAnimation[] groups)
    {
        foreach(GroupAnimation group in groups)
        {
            group.ToggleUseGravity();
        }
    }

    public void ToggleUseGravity(GroupAnimation group)
    {
        group.ToggleUseGravity();
    }

    public void SetUseGravity(bool useGravity, params GroupAnimation[] groups)
    {
        foreach(GroupAnimation group in groups)
        {
            group.SetUseGravity(useGravity);
        }
    }

    public void SetUseGravity(GroupAnimation group, bool useGravity)
    {
        group.SetUseGravity(useGravity);
    }

    public void ChangeGravity(Vector3 newGravity)
    {
        Physics.gravity = newGravity;
    }

    public void Pulse(GroupAnimation group)
    {
        group.pulseGroup = true;
    }

    public void FadeVideo(GroupAnimation group, float fadeAmount, bool fadein, bool epilepsySafe)
    {
        FadeVideo(group, group.fadeSpeed, fadeAmount, fadein, epilepsySafe);
    }

    public void FadeVideo(GroupAnimation group, bool fadein, bool epilepsySafe)
    {
        FadeVideo(group, group.fadeSpeed, 1.0f, fadein, epilepsySafe);
    }

    public void FadeVideo(GroupAnimation group, float speed, float fadeAmount, bool fadein, bool epilepsySafe)
    {
        if (!epilepsySafe)
        {
            group.fadeAmount = fadeAmount;
            if (fadein)
            {
                group.fadeToVideo = true;
            }
            else
            {
                group.fadeToBG = true;
            }
        }
    }

    public void FadeColor(GroupAnimation group, float speed, float delay)
    {
        StartCoroutine(DoFadeTransparency(group, speed, delay));
    }

    private IEnumerator DoFadeTransparency(GroupAnimation group, float speed, float delay)
    {
        Image img = group.GetComponentInChildren<Image>();
        for (float i = 0; i <= delay; i += Time.deltaTime)
        {
            //wait for delay
            yield return null;
        }
        for (float i = 0; i <= speed; i += Time.deltaTime)
        {
            // set color with i as alpha
            img.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }

    public void MakeTransparent(GroupAnimation group)
    {
        Image img = group.GetComponentInChildren<Image>();
        img.color = new Color(1, 1, 1, 0);
    }

    public void Delete(params GroupAnimation[] groups)
    {
        foreach(GroupAnimation group in groups)
        {
            group.Delete(group.resizeSpeed);
        }
    }

    public void Delete(GroupAnimation group, float speed)
    {
        group.Delete(speed);
    }
}
