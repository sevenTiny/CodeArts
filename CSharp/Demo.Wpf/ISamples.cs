namespace Demo.Wpf
{
    public interface ISamples
    {
        /// <summary>
        /// 标题
        /// </summary>
        string SampleTitle { get; }
        /// <summary>
        /// 描述
        /// </summary>
        string SampleDescription { get; }
        /// <summary>
        /// 启动方法
        /// </summary>
        void SampleStart();
    }
}
