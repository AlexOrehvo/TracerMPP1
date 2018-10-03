namespace Tracer_mpp
{
    public interface IWriter
    {
        void Write(string data);
		void Write(string fileName, string data);
	}
}