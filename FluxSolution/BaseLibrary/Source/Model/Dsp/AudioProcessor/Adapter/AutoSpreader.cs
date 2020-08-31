namespace Flux.Dsp.AudioProcessor.Adapter
{
  /// <summary>Add as many effects as desired, and they will be applied evenly across the stereo spectrum.</summary>
  public class AutoSpreader
    : IAudioProcessorStereo
  {
    public System.Collections.Generic.List<IAudioProcessorMono> Processors { get; } = new System.Collections.Generic.List<IAudioProcessorMono>();

    public StereoSample ProcessAudio(StereoSample sample)
    {
      var left = sample.FrontLeft;
      var right = sample.FrontRight;

      var mono = Convert.StereoToMono(left, right);

      var gapSize = 2.0 / (Processors.Count - 1); // Compute a distribution gap size spread evenly across the stereo field.

      for (var i = 0; i < Processors.Count; i++)
      {
        if (Processors[i] as IAudioProcessorMono is var processor && processor != null)
        {
          var sampleM = processor.ProcessAudio(mono);

          var stereo = StereoPan.ApplyStereoPan(-1.0 + gapSize * i, new StereoSample(sampleM, sampleM));

          left += stereo.FrontLeft;
          right += stereo.FrontRight;
        }
      }

      return new StereoSample(left / Processors.Count, right / Processors.Count);
    }
  }
}
