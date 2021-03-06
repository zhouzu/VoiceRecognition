using System.Speech.Internal.SrgsParser;
using System.Speech.Recognition;

namespace System.Speech.Internal.GrammarBuilding
{
	internal sealed class SemanticKeyElement : BuilderElements
	{
		private readonly string _semanticKey;

		private readonly RuleRefElement _ruleRef;

		internal override string DebugSummary => _ruleRef.Rule.DebugSummary;

		internal SemanticKeyElement(string semanticKey)
		{
			_semanticKey = semanticKey;
			RuleElement ruleElement = new RuleElement(semanticKey);
			_ruleRef = new RuleRefElement(ruleElement, _semanticKey);
			base.Items.Add(ruleElement);
			base.Items.Add(_ruleRef);
		}

		public override bool Equals(object obj)
		{
			SemanticKeyElement semanticKeyElement = obj as SemanticKeyElement;
			if (semanticKeyElement == null)
			{
				return false;
			}
			if (!base.Equals(obj))
			{
				return false;
			}
			return _semanticKey == semanticKeyElement._semanticKey;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		internal new void Add(string phrase)
		{
			_ruleRef.Add(new GrammarBuilderPhrase(phrase));
		}

		internal new void Add(GrammarBuilder builder)
		{
			foreach (GrammarBuilderBase item in builder.InternalBuilder.Items)
			{
				_ruleRef.Add(item);
			}
		}

		internal override GrammarBuilderBase Clone()
		{
			SemanticKeyElement semanticKeyElement = new SemanticKeyElement(_semanticKey);
			semanticKeyElement._ruleRef.CloneItems(_ruleRef);
			return semanticKeyElement;
		}

		internal override IElement CreateElement(IElementFactory elementFactory, IElement parent, IRule rule, IdentifierCollection ruleIds)
		{
			_ruleRef.Rule.CreateElement(elementFactory, parent, rule, ruleIds);
			return _ruleRef.CreateElement(elementFactory, parent, rule, ruleIds);
		}
	}
}
