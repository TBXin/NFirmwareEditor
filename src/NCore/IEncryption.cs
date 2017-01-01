namespace NCore
{
	public interface IEncryption
	{
		EncryptionType Type { get; }

		byte[] Encode(byte[] source);

		byte[] Decode(byte[] source);
	}

	public enum EncryptionType
	{
		None,
		Joyetech,
		ArcticFox
	}
}
