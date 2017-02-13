namespace NCore
{
	public interface IEncryption
	{
		byte[] Encode(byte[] source);

		byte[] Decode(byte[] source);
	}

	public enum EncryptionType
	{
		None,
		Joyetech,
		ArcticFox,
		ArcticFox2
	}
}
