using System;
using System.Globalization;
using System.Text;
using AdvancedCalculator;

namespace CalculatorTypes {

    public interface IInverseable<out T> {
        T Inverse();
    }

    public class Real : IComparable<Real>, IEquatable<Real>, ICalculatorable<Real, Real>, INegativable<Real>, IInverseable<Real> {
        private readonly double value = 0;

        public Real() { }

        public Real(double val) {
            value = val;
        }

        public static implicit operator double(Real real) {
            return real.value;
        }

        public static implicit operator Real(double dbl) {
            return new Real(dbl);
        }

        public int CompareTo(Real other) {
            return value.CompareTo(other.value);
        }

        public bool Equals(Real other) {
            return this == other;
        }

        public Real Plus(Real other) {
            return new Real(value + other.value);
        }

        public Real Minus(Real other) {
            return new Real(value - other.value);
        }

        public Real Divide(Real other) {
            return new Real(value / other.value);
        }

        public Real Multiply(Real other) {
            return new Real(value * other.value);
        }

        public Real Negative() {
            return new Real(value * -1);
        }

        public static bool operator ==(Real r1, Real r2) {
            if (ReferenceEquals(r1, null) || ReferenceEquals(r2, null)) {
                return ReferenceEquals(r1, null) && ReferenceEquals(r2, null);
            }
            return Math.Abs(r1.value - r2.value) < Double.Epsilon;
        }

        public static bool operator !=(Real r1, Real r2) {
            return !(r1 == r2);
        }

        public static bool operator <(Real r1, Real r2) {
            return r1.value < r2.value;
        }

        public static bool operator >(Real r1, Real r2) {
            return r1.value > r2.value;
        }

        public override string ToString() {
            return Convert.ToString(value);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            if (obj.GetType() != GetType()) {
                return false;
            }
            return Equals((Real)obj);
        }

        public override int GetHashCode() {
            return value.GetHashCode();
        }

        public Real Inverse() {
            return new Real(new Real(1).Divide(this));
        }
    }

    public class Complex : IEquatable<Complex>, ICalculatorable<Complex, Complex>, INegativable<Complex>,
        IInverseable<Complex> {
        private readonly double re = 0;
        private readonly double im = 0;

        public Complex() { }

        public Complex(double rl, double img = 0) {
            re = rl;
            im = img;
        }

        public static implicit operator double(Complex complex) {
            return complex.re;
        }

        public static implicit operator Complex(double dbl) {
            return new Complex(dbl);
        }

        public bool Equals(Complex other) {
            return this == other;
        }

        public Complex Plus(Complex other) {
            return new Complex(re + other.re, im + other.im);
        }

        public Complex Minus(Complex other) {
            return new Complex(re - other.re, im - other.im);
        }

        public Complex Divide(Complex other) {
            if (other.re < Double.Epsilon && other.im < Double.Epsilon) {
                throw new DivideByZeroException();
            }
            return new Complex((re * other.re + im * other.im) / (other.re * other.re + other.im * other.im),
                (other.re * im - other.im * re) / (other.re * other.re + other.im * other.im));
        }

        public Complex Multiply(Complex other) {
            return new Complex(re * other.re - im * other.im, re * other.im + im * other.re);
        }


        public Complex Negative() {
            return new Complex(re * -1, im * -1);
        }

        public static bool operator ==(Complex r1, Complex r2) {
            if (ReferenceEquals(r1, null) || ReferenceEquals(r2, null)) {
                return ReferenceEquals(r1, null) && ReferenceEquals(r2, null);
            }
            return Math.Abs(r1.re - r2.re) < Double.Epsilon && Math.Abs(r1.im - r2.im) < Double.Epsilon;
        }

        public static bool operator !=(Complex r1, Complex r2) {
            return !(r1 == r2);
        }

        public override string ToString() {
            if (Math.Abs(im) < Double.Epsilon) return re.ToString(CultureInfo.InvariantCulture);
            return re + (im >= 0 ? "+i" : "-i") + Math.Abs(im);
        }

        public Complex Inverse() {
            return new Complex(new Complex(1).Divide(this));
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Complex)obj);
        }

        public override int GetHashCode() {
            unchecked {
                return (re.GetHashCode() * 397) ^ im.GetHashCode();
            }
        }
    }

    public class Matrix<T> : IEquatable<Matrix<T>>, ICalculatorable<Matrix<T>, Matrix<T>>, ICalculatorable<T, Matrix<T>>, INegativable<Matrix<T>>
        where T : ICalculatorable<T, T>, INegativable<T>, IInverseable<T>, new() {
        public int height { get; private set; }
        public int width { get; private set; }

        private T[,] _array;

        public T[,] array {
            get { return _array; }
            set {
                _array = value;
                if (value != null) {
                    height = value.GetUpperBound(0) + 1;
                    width = value.GetUpperBound(1) + 1;
                } else {
                    _array = null;
                    height = width = 0;
                }
            }
        }

        public Matrix() { }

        public Matrix(T[,] mtr) {
            array = mtr;
        }

        private Matrix(int h, int w) {
            array = new T[h, w];
            height = h;
            width = w;
        }

        public static Matrix<T> FromArray(params T[][] prm) {
            if (prm == null || prm[0] == null) {
                return null;
            }

            int leng = prm[0].Length;
            Matrix<T> tmp = new Matrix<T>(prm.Length, leng);

            for (int i = 0; i < prm.Length; ++i) {
                if (prm[i].Length != leng) {
                    throw new Exception("Wrong length");
                }
                for (int j = 0; j < leng; ++j) {
                    tmp.array[i, j] = prm[i][j];
                }
            }
            return tmp;
        }

        public Matrix(Matrix<T> other) {
            height = other.height;
            width = other.width;

            array = new T[other.height, other.width];
            for (int i = 0; i < height; ++i) {
                for (int j = 0; j < width; ++j) {
                    array[i, j] = other.array[i, j];
                }
            }
        }

        public override string ToString() {
            StringBuilder strb = new StringBuilder();
            if (height * width < 900) {
                for (int i = 0; i < height; ++i) {
                    for (int j = 0; j < width; ++j) {
                        strb.Append(array[i, j]).Append(" ");
                    }
                    strb.Append("\n");
                }
            } else {
                strb.Append("Matrix<").Append(typeof(T).Name).Append(">[");
                strb.Append(height).Append("x").Append(width).Append("]");
            }
            return strb.ToString();
        }

        public Matrix<T> Plus(Matrix<T> other) {
            if (width != other.width || height != other.height) {
                throw new Exception("Wrong matrix size");
            }
            Matrix<T> res = new Matrix<T>(height, width);
            for (int i = 0; i < height; ++i) {
                for (int j = 0; j < width; ++j) {
                    res.array[i, j] = array[i, j].Plus(other.array[i, j]);
                }
            }
            return res;
        }

        public Matrix<T> Minus(Matrix<T> other) {
            if (width != other.width || height != other.height) {
                throw new Exception("Wrong matrix size");
            }
            Matrix<T> res = new Matrix<T>(height, width);
            for (int i = 0; i < height; ++i) {
                for (int j = 0; j < width; ++j) {
                    res.array[i, j] = array[i, j].Minus(other.array[i, j]);
                }
            }
            return res;
        }

        public Matrix<T> Divide(Matrix<T> other) {
            throw new NotImplementedException();
        }

        public Matrix<T> Multiply(Matrix<T> other) {
            if (width != other.height) {
                throw new Exception("Wrong matrix size");
            }
            Matrix<T> res = new Matrix<T>(height, other.width);
            for (int i = 0; i < height; ++i) {
                for (int j = 0; j < other.width; ++j) {
                    T sum = new T();
                    for (int k = 0; k < width; ++k) {
                        sum = sum.Plus(array[i, k].Multiply(other.array[k, j]));
                    }
                    res.array[i, j] = sum;
                }
            }
            return res;
        }

        public Matrix<T> Plus(T other) {
            throw new NotImplementedException();
        }

        public Matrix<T> Minus(T other) {
            throw new NotImplementedException();
        }

        public Matrix<T> Divide(T other) {
            return new Matrix<T>(this).Multiply(other.Inverse());
        }

        public Matrix<T> Multiply(T other) {
            Matrix<T> res = new Matrix<T>(height, width);
            for (int i = 0; i < height; ++i) {
                for (int j = 0; j < width; ++j) {
                    res.array[i, j] = other.Multiply(array[i, j]);
                }
            }
            return res;
        }

        public Matrix<T> Negative() {
            Matrix<T> res = new Matrix<T>(height, width);
            for (int i = 0; i < height; ++i) {
                for (int j = 0; j < width; ++j) {
                    res.array[i, j] = array[i, j].Negative();
                }
            }
            return res;
        }

        public Matrix<T> Transposed() {
            Matrix<T> trns = new Matrix<T>(width, height) { array = new T[width, height] };
            for (int i = 0; i < width; ++i) {
                for (int j = 0; j < height; ++j) {
                    trns.array[i, j] = array[j, i];
                }
            }
            return trns;
        }

        public T Trace() {
            if (height != width) {
                throw new Exception("Wrong size");
            }
            T sum = new T();
            for (int i = 0; i < width; ++i) {
                sum = sum.Plus(array[i, i]);
            }
            return sum;
        }

        private class ValueWrapper<TV> {
            public TV Value;

            public ValueWrapper(TV val) {
                Value = val;
            }
        }

        private void multiplyRow(int row, T number) {
            for (int i = 0; i < width; ++i) {
                array[row, i] = array[row, i].Multiply(number);
            }
        }

        private void rowPlusRowMult(int dest, int src, T mlt) {
            for (int i = 0; i < width; ++i) {
                array[dest, i] = array[dest, i].Plus(array[src, i].Multiply(mlt));
            }
        }

        public T Determinant() {
            Matrix<T> tmp = new Matrix<T>(this);
            ValueWrapper<T> mult = null;
            T zero = new T();

            for (int i = 0; i < width; ++i) {
                int rw = -1;
                for (int j = i; j < height; j++) {
                    bool flg = true;
                    for (int k = 0; k < i; ++k) {
                        if (!tmp.array[j, k].Equals(zero)) {
                            flg = false;
                        }
                    }
                    if (!tmp.array[j, i].Equals(zero) && flg) {
                        rw = j;
                        break;
                    }
                }
                if (rw == -1) {
                    return zero;
                }

                for (int j = i; j < height; j++) {
                    if (!tmp.array[j, i].Equals(zero) && j != rw) {
                        T zn = tmp.array[j, i];
                        tmp.multiplyRow(j, tmp.array[rw, i]);
                        if (mult == null) {
                            mult = new ValueWrapper<T>(tmp.array[rw, i]);
                        } else {
                            mult.Value = mult.Value.Multiply(tmp.array[rw, i]);
                        }
                        tmp.rowPlusRowMult(j, rw, zn.Negative());
                    }
                }
            }
            T det = tmp.array[0, 0];

            for (int i = 1; i < width; ++i) {
                det = det.Multiply(tmp.array[i, i]);
            }
            return ReferenceEquals(mult, null) ? det : det.Divide(mult.Value);
        }

        public bool Equals(Matrix<T> other) {
            if (other.width != width || other.height != height) return false;

            if (ReferenceEquals(other._array, null) || ReferenceEquals(_array, null)) {
                return ReferenceEquals(other._array, null) && ReferenceEquals(_array, null);
            }

            for (int i = 0; i < height; ++i) {
                for (int j = 0; j < width; ++j) {
                    if (!_array[i, j].Equals(other._array[i, j])) return false;
                }
            }
            return true;
        }

    }

    public class Polinom<T> : IEquatable<Polinom<T>>, ICalculatorable<Polinom<T>, Polinom<T>>,
        ICalculatorable<T, Polinom<T>>, INegativable<Polinom<T>>
        where T : ICalculatorable<T, T>, INegativable<T>, IInverseable<T>, IEquatable<T>, new() {
        private T[] coefficients;

        public int size {
            get { return coefficients.Length; }
        }

        public T this[int indx] {
            get { return coefficients[indx]; }
        }

        public Polinom(T[] coeff) {
            coefficients = coeff;
        }

        public Polinom<T> Plus(T other) {
            Polinom<T> res = new Polinom<T>(new T[size]);
            for (int i = 0; i < size; ++i) {
                res.coefficients[i] = coefficients[i];
            }
            res.coefficients[0] = coefficients[0].Plus(other);
            res.Simplify();
            return res;
        }

        public Polinom<T> Minus(T other) {
            Polinom<T> res = new Polinom<T>(new T[size]);
            for (int i = 0; i < size; ++i) {
                res.coefficients[i] = coefficients[i];
            }
            res.coefficients[0] = coefficients[0].Minus(other);
            res.Simplify();
            return res;
        }

        public Polinom<T> Divide(T other) {
            Polinom<T> res = new Polinom<T>(new T[size]);
            for (int i = 0; i < size; ++i) {
                res.coefficients[i] = coefficients[i].Divide(other);
            }
            res.Simplify();
            return res;
        }

        public Polinom<T> Multiply(T other) {
            Polinom<T> res = new Polinom<T>(new T[size]);
            for (int i = 0; i < size; ++i) {
                res.coefficients[i] = coefficients[i].Multiply(other);
            }
            res.Simplify();
            return res;
        }

        public Polinom<T> Plus(Polinom<T> other) {
            Polinom<T> res = new Polinom<T>(new T[Math.Max(size, other.size)]);
            if (size > other.size) {
                for (int i = 0; i < other.size; ++i) {
                    res.coefficients[i] = coefficients[i].Plus(other.coefficients[i]);
                }
                for (int i = other.size; i < size; ++i) {
                    res.coefficients[i] = coefficients[i];
                }
            } else {
                for (int i = 0; i < size; ++i) {
                    res.coefficients[i] = coefficients[i].Plus(other.coefficients[i]);
                }
                for (int i = size; i < other.size; ++i) {
                    res.coefficients[i] = other.coefficients[i];
                }
            }
            res.Simplify();
            return res;
        }

        public Polinom<T> Minus(Polinom<T> other) {
            return Plus(other.Negative());
        }

        public Polinom<T> Divide(Polinom<T> other) {
            throw new NotImplementedException();
        }

        public Polinom<T> Multiply(Polinom<T> other) {
            Polinom<T> res = new Polinom<T>(new T[size + other.size - 1]);
            for (int i = 0; i < res.size; ++i) {
                res.coefficients[i] = new T();
            }
            for (int i = 0; i < size; ++i) {
                for (int j = 0; j < other.size; ++j) {
                    res.coefficients[i + j] =
                        res.coefficients[i + j].Plus(coefficients[i].Multiply(other.coefficients[j]));
                }
            }
            res.Simplify();
            return res;
        }

        public Polinom<T> Negative() {
            Polinom<T> res = new Polinom<T>(new T[coefficients.Length]);
            for (int i = 0; i < coefficients.Length; ++i) {
                res.coefficients[i] = coefficients[i].Negative();
            }
            return res;
        }

        private void Simplify() {
            int i = size - 1;
            T zero = new T();
            for (; i >= 0; --i) {
                if (!coefficients[i].Equals(zero)) {
                    break;
                }
            }
            if (i == -1) {
                coefficients = new T[1];
                coefficients[0] = new T();
                return;
            }
            if (i == size - 1) {
                return;
            }
            T[] newCoeff = new T[i + 1];
            for (int j = 0; j < i + 1; ++j) {
                newCoeff[j] = coefficients[j];
            }
            coefficients = newCoeff;
        }

        public bool Equals(Polinom<T> other) {
            if (size != other.size) return false;

            if (ReferenceEquals(other.coefficients, null) || ReferenceEquals(coefficients, null)) {
                return ReferenceEquals(other.coefficients, null) && ReferenceEquals(coefficients, null);
            }

            for (int i = 0; i < size; ++i) {
                if (!other.coefficients[i].Equals(coefficients[i])) return false;
            }
            return true;
        }

        public override string ToString() {
            if (coefficients == null) return "null";

            T zero = new T();
            var strb = new StringBuilder();
            bool wasSmth = false;

            for (int i = 0; i < size; ++i) {
                if (coefficients[i].Equals(zero)) continue;

                strb.Append("(").Append(coefficients[i]).Append(")");
                if (i > 0) {
                    strb.Append("x");
                    if (i > 1) strb.Append("^").Append(i);
                }
                strb.Append((i != size - 1 ? " + " : ""));
                wasSmth = true;
            }

            return !wasSmth ? "0" : strb.ToString();
        }
    }
}