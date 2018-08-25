namespace EnaBricks.Generics
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public sealed class Option<T> : IEnumerable<T>
    {
        private readonly T[] _entity;

        private Option(T[] content)
        {
            _entity = content;
        }

        public static Option<T> Some(T value) =>
            new Option<T>(new[] { value });

        public static Option<T> None() =>
            new Option<T>(Array.Empty<T>());

        public IEnumerator<T> GetEnumerator() =>
            ((IEnumerable<T>)this._entity).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }
}
