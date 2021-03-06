using System.Runtime.InteropServices;

namespace System.Speech.Internal.SapiInterop
{
	[ComImport]
	[Guid("4B37BC9E-9ED6-44a3-93D3-18F022B79EC3")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface ISpRecoGrammar2
	{
		void GetRules(out IntPtr ppCoMemRules, out uint puNumRules);

		void LoadCmdFromFile2([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, SPLOADOPTIONS Options, [MarshalAs(UnmanagedType.LPWStr)] string pszSharingUri, [MarshalAs(UnmanagedType.LPWStr)] string pszBaseUri);

		void LoadCmdFromMemory2(IntPtr pGrammar, SPLOADOPTIONS Options, [MarshalAs(UnmanagedType.LPWStr)] string pszSharingUri, [MarshalAs(UnmanagedType.LPWStr)] string pszBaseUri);

		void SetRulePriority([MarshalAs(UnmanagedType.LPWStr)] string pszRuleName, uint ulRuleId, int nRulePriority);

		void SetRuleWeight([MarshalAs(UnmanagedType.LPWStr)] string pszRuleName, uint ulRuleId, float flWeight);

		void SetDictationWeight(float flWeight);

		void SetGrammarLoader(ISpGrammarResourceLoader pLoader);

		void Slot2();
	}
}
