using UnityEngine;

/// <summary>
/// Скрипт для воспроизведения пространственных звуков (взрывы, выстрелы).
/// Создаёт временный GameObject с AudioSource, 
/// копирует параметры из исходного источника,
/// проигрывает звук и автоматически удаляет объект по окончании аудиоклипа.
/// </summary>

public static class ShipDestroyAndFireSound
{
    public static void PlayExplosionAtPoint(AudioSource source, Vector3 position)
    {
        if (source == null || source.clip == null)
            return;

        GameObject go = new GameObject("ShipExplosionSound");
        go.transform.position = position;

        AudioSource newSource = go.AddComponent<AudioSource>();
        newSource.clip = source.clip;
        newSource.volume = source.volume;
        newSource.spatialBlend = source.spatialBlend;
        newSource.rolloffMode = source.rolloffMode;

        newSource.Play();

        Object.Destroy(go, source.clip.length);
    }

    public static void PlayShotAtPoint(AudioSource source, Vector3 position)
    {
        if (source == null || source.clip == null)
            return;

        GameObject go = new GameObject("ShipShotSound");
        go.transform.position = position;

        AudioSource newSource = go.AddComponent<AudioSource>();
        newSource.clip = source.clip;
        newSource.volume = source.volume;
        newSource.spatialBlend = source.spatialBlend;
        newSource.rolloffMode = source.rolloffMode;

        newSource.Play();

        Object.Destroy(go, source.clip.length);
    }
}