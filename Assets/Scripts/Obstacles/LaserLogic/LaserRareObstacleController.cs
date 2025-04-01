public class LaserRareObstacleController : AbstractRareObstacle
{
    private void OnEnable()
    {
        LaserGeneratorMover.DisableLaserObstacle += Disable;
    }

    private void OnDisable()
    {
        LaserGeneratorMover.DisableLaserObstacle -= Disable;
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
