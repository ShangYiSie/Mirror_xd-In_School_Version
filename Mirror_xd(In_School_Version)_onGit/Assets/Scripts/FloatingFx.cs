using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingFx : MonoBehaviour
{
    public Vector3 offset;
    public float frequency;
    public bool playAwake;

    private Vector3 originPosition;
    private float tick;
    private float amplitude;
    private bool animate;

    void Awake()
    {
        // 如果沒有設置頻率或設置的頻率為0則自動記錄成1
        if (Mathf.Approximately(frequency, 0))
            frequency = 1f;

        originPosition = transform.localPosition;
        tick = Random.Range(0f, 2f * Mathf.PI);
        // 計算震幅
        amplitude = 2 * Mathf.PI / frequency;
        animate = playAwake;
    }

    public void Play()
    {
        transform.localPosition = originPosition;
        animate = true;
    }

    public void Stop()
    {
        transform.localPosition = originPosition;
        animate = false;
    }

    void FixedUpdate()
    {
        if (animate)
        {
            // 計算下一個時間量
            tick = tick + Time.fixedDeltaTime * amplitude;
            // 計算下一個偏移量
            var amp = new Vector3(Mathf.Cos(tick) * offset.x, Mathf.Sin(tick) * offset.y, 0);
            // 更新坐標
            transform.localPosition = originPosition + amp;
        }
    }

}
